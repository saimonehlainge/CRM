using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Helpers;
using Recruitment.Repositories;
using Recruitment.Services;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppContextConnection") ?? throw new InvalidOperationException("Connection string 'RecruitmentContextConnection' not found.");

builder.Services.AddDbContext<RecruitmentContext>(options =>
    options.UseSqlServer(connectionString,
    sqlServerOptionsAction: sqlOption =>
    {
        sqlOption.EnableRetryOnFailure(
        maxRetryCount: 10,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
    })
);

builder.Services.AddIdentity<RecruitmentUser, Role>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        //options.User.RequireUniqueEmail = true;
        options.User.RequireUniqueEmail = false;
        options.Lockout.MaxFailedAccessAttempts = 50;
    })
    .AddEntityFrameworkStores<RecruitmentContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddRoles<Role>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings  
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "Horus";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/account/logout";
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddMvc()
    .AddRazorPagesOptions(option => option.Conventions.AddPageRoute("/Frontend/Index", ""));

//builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.Configure<KestrelServerOptions>(option =>
{
    option.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
 });


AddScoped();

builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)

    .AddCertificate(options =>
    {
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = context =>
            {
                var claims = new[]
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        context.ClientCertificate.Subject,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer),
                    new Claim(
                        ClaimTypes.Name,
                        context.ClientCertificate.Subject,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer),
                    new Claim(
                        "Department",
                        "",
                        ""
                        )
                    };
                context.Principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, context.Scheme.Name));
                context.Success();
                return Task.CompletedTask;
            }
        };
    })
    .AddCookie(op =>
    {
        op.SlidingExpiration = true;
        op.ExpireTimeSpan = TimeSpan.FromDays(1);
        op.AccessDeniedPath = "/Identity/Account/Login";
        op.LoginPath = "/Identity/Account/Login";
        op.LogoutPath = "/Identity/Account/Login";
        op.Cookie.Expiration = TimeSpan.FromDays(1);
        op.Cookie.Name = ".Orange";
    });

// อ่านจาก appsettings.json
//var licenseContext = builder.Configuration["EPPlus:ExcelPackage:LicenseContext"];

//ExcelPackage.LicenseContext = licenseContext?.Equals("Commercial", StringComparison.OrdinalIgnoreCase) == true
//    ? LicenseContext.Commercial
//    : LicenseContext.NonCommercial;

//var loggerFactory = app.Services.GetService<ILoggerFactory>();
//loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"]);

//การตั้งค่าการบันทึก log
builder.Logging.ClearProviders();  // ลบ provider ที่มีอยู่แล้ว
builder.Logging.AddSerilog(new LoggerConfiguration()
    .WriteTo.File(
        "logs/log-.log", // ใช้ {Date} ในชื่อไฟล์เพื่อใส่วันที่
        rollingInterval: RollingInterval.Day  // สร้างไฟล์ใหม่ทุกวัน
    )
    .CreateLogger());

// การกรอง log ให้บันทึกเฉพาะ Error ขึ้นไป
builder.Logging
    .AddFilter("Microsoft", LogLevel.Error)  // กรองเฉพาะ log ที่ระดับ Error ขึ้นไปใน Microsoft namespace
    .AddFilter("System", LogLevel.Error)     // กรองเฉพาะ log ที่ระดับ Error ขึ้นไปใน System namespace
    .AddFilter("YourNamespace", LogLevel.Error);  // กรองเฉพาะ log ที่ระดับ Error ขึ้นไปใน namespace ของคุณ

var keyPath = Path.Combine(builder.Environment.ContentRootPath, "keys");

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(keyPath))
    .SetApplicationName("RecruiterApp");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseCheckTimeout();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapRazorPages();
app.UseEndpoints(option =>
{
    option.MapRazorPages();
});
app.MapHub<NotificationHub>("/notificationHub");
app.Run();

void AddScoped()
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<ILoggerHelperRepository, LoggerHelper>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddSingleton<INotificationHub, NotificationRepository>();
}
