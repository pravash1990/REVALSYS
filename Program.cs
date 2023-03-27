using Data;
using Models;
using Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System;

        var builder = WebApplication.CreateBuilder(args);

        ConfigurationManager configuration = builder.Configuration;       
        IConfigurationSection identityDefaultOptionsConfigurationSection = configuration.GetSection("IdentityDefaultOptions");
        var identityDefaultOptions = identityDefaultOptionsConfigurationSection.Get<IdentityDefaultOptions>();

      
        builder.Services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfigurationSection);        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                      options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllersWithViews().AddJsonOptions(options=> options.JsonSerializerOptions.PropertyNamingPolicy=null);
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        // Set the JSON serializer options
        builder.Services.AddMvc().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
            // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
            options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
            options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
            options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
            options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
            options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
            options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
            options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;
            // User settings
            options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;
            // email confirmation require
            options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = identityDefaultOptions.CookieHttpOnly;
   //  options.Cookie.Expiration = TimeSpan.FromDays(identityDefaultOptions.CookieExpiration);
   options.ExpireTimeSpan = TimeSpan.FromDays(identityDefaultOptions.CookieExpiration);
    options.LoginPath = identityDefaultOptions.LoginPath; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
    options.LogoutPath = identityDefaultOptions.LogoutPath; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
    options.AccessDeniedPath = identityDefaultOptions.AccessDeniedPath; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
    options.SlidingExpiration = identityDefaultOptions.SlidingExpiration;
});

// Get SendGrid configuration options
builder.Services.Configure<SendGridOptions>(configuration.GetSection("SendGridOptions"));
        // Get SMTP configuration options
       
        // Get Super Admin Default options
        builder.Services.Configure<SuperAdminDefaultOptions>(configuration.GetSection("SuperAdminDefaultOptions"));

        // Add email services.
        builder.Services.AddTransient<INumberSequence, Services.NumberSequence>();
        builder.Services.AddTransient<IRoles, Roles>();
        builder.Services.AddTransient<IFunctional, Functional>();      
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.Cookie.MaxAge = options.ExpireTimeSpan; // optional
            options.SlidingExpiration = true;
        });
        var app = builder.Build();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();            
        }
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=UserRole}/{action=UserProfile}/{id?}");
        app.Run();
