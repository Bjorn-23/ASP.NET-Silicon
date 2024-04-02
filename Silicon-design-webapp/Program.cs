using Business.Services;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silicon_design_webapp.Helpers;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";

    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;

});

builder.Services.AddAuthorization( x => 
{
    x.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
    x.AddPolicy("CIO", policy => policy.RequireRole("SuperAdmin", "CIO"));
    x.AddPolicy("Admin", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin"));
    x.AddPolicy("Manager", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "Manager"));
    x.AddPolicy("User", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "Manager", "User"));
});

var configurate = builder.Configuration;

builder.Services.AddAuthentication()
.AddFacebook(x =>
{
    x.AppId = configurate["Authentication:Facebook:ClientId"]!;
    x.AppSecret = configurate["Authentication:Facebook:ClientSecret"]!;
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
})
.AddGoogle(x =>
{
    x.ClientId = configurate["Authentication:Google:ClientId"]!;
    x.ClientSecret = configurate["Authentication:Google:ClientSecret"]!;

});

builder.Services.AddHttpClient();
builder.Services.AddSession( x=>
{
    x.IdleTimeout = TimeSpan.FromMinutes(20);
    x.Cookie.IsEssential = true;
    //x.Cookie.HttpOnly = true;

});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<AdminService>();


//------------------------------------------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------------------------------------------


var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error/404", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseStaticFiles();


app.UseAuthentication();
app.UseUserSessionValidationMiddleware();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["SuperAdmin", "CIO","Admin", "Manager", "User"];

    foreach (var role in roles)
    if (!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }
}

    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
