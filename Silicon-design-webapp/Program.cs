using Business.Services;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using Silicon_design_webapp.Helpers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ApplicationDbContext>();

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

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();




var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();


app.UseAuthentication();
app.UseUserSessionValidationMiddleware();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/error/404", "?statusCode={0}");

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseNotFoundMiddleware();

app.Run();
