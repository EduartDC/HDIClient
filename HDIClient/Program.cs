using HDIClient.Service;
using HDIClient.Service.Interface;

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
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddMemoryCache();

var appSettings = builder.Configuration.GetSection("ApiSettings");
builder.Services.AddHttpClient("ApiHttpClient", client =>
{
    client.BaseAddress = new Uri(appSettings.GetValue<string>("BaseAddress"));
});

builder.Services.AddDistributedMemoryCache();
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

app.UseRouting();

app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Login}/{action=LoginView}");

app.MapControllerRoute(
    name: "registerdriver",
    pattern: "{controller=RegisterDriver}/{action=RegisterDriverDos}");

app.MapControllerRoute(
    name: "registerEmployee",
    pattern: "{controller=RegisterEmployee}/{action=RegisterEmployee}");


app.Run();
