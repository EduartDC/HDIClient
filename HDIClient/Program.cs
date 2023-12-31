using HDIClient.Service;
using HDIClient.Service.Interface;


using Microsoft.AspNetCore.Authentication.Cookies;

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
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddMemoryCache();
// Add JWT authentication

var appSettings = builder.Configuration.GetSection("ApiSettings");

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAccidentService, AccidentService>();
builder.Services.AddMemoryCache();

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "HDIClientCookie";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.LoginPath = "/Account/LoginView";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddControllersWithViews();
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");


app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=LoginView}");
app.MapControllerRoute(
    name: "report",
    pattern: "{controller=Report}/{action=Tips}");
app.MapControllerRoute(
    name: "policy",
    pattern: "{controller=Policy}/{action=ViewPolicy}");

app.MapControllerRoute(
    name: "registerdriver",
    pattern: "{controller=RegisterDriver}/{action=RegisterDriverDos}");

app.MapControllerRoute(
    name: "registerEmployee",
    pattern: "{controller=RegisterEmployee}/{action=GetRegisterEmployeeView}");
app.MapControllerRoute(
    name: "editEmployee",
    pattern: "{controller=EditEmployee}/{action=EditRegisterEmployeeView}");
app.MapControllerRoute(
    name: "employeeManagement",
    pattern: "{controller=EmployeeManagement}/{action=EmployeeManagementView}");
app.MapControllerRoute(
    name: "showReportsAdjuster",
    pattern: "{controller=ShowReportsAdjuster}/{action=ShowReportsAjusterView}");
app.MapControllerRoute(
    name: "sinister",
    pattern: "{controller=Sinister}/{action=ConsultClaimHistory}");
app.MapControllerRoute(
    name: "sinister",
       pattern: "{controller=Sinister}/{action=AssignLossAdjuster}");
app.MapControllerRoute(
    name: "sinister",
          pattern: "{controller=Sinister}/{action=AssignLossAdjusterExtend}");


app.Run();
