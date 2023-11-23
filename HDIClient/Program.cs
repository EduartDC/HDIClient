using HDIClient.Service;
using HDIClient.Service.Interface;
using HDIClient.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthorizationFilter>();
builder.Services.AddMemoryCache();
// Add JWT authentication
var appSettings = builder.Configuration.GetSection("ApiSettings");



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient("ApiHttpClient", client =>
{
    client.BaseAddress = new Uri(appSettings.GetValue<string>("BaseAddress"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// Add authentication and authorization middleware
app.UseAuthentication();


app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Login}/{action=LoginView}");
app.MapControllerRoute(
    name: "report",
    pattern: "{controller=NewReport}/{action=NewReportView}");


app.Run();
