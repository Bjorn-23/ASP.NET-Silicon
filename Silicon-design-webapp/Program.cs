using Business.Services;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.EntityFrameworkCore;
using Silicon_design_webapp.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();


//add services here such as repositories, services, dbcontext etc.
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddAuthentication("AuthCookie").AddCookie("AuthCookie", x =>
{

    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.Cookie.HttpOnly = true;
});

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();




var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();
app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseUserSessionValidationMiddleware();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseNotFoundMiddleware();

app.Run();
