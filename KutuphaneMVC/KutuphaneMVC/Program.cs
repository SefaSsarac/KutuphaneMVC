using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login"); // Entry point for login
        options.LogoutPath = new PathString("/Account/Logout"); // Path for logout
        options.AccessDeniedPath = new PathString("/Account/AccessDenied"); // Path for access denied
    });

var app = builder.Build();

// Enable authentication middleware
app.UseAuthentication();

// Add static files
app.UseStaticFiles();

// Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
