using Business.Services;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Identity;

//using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Silicon_design_webapp.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();


//add services here such as repositories, services, dbcontext etc.
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddAuthentication("AuthCookie").AddCookie("AuthCookie", x =>
{
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<UserManager<UserEntity>>();  // not necessary?




var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
//app.UseNotFoundMiddleware();

app.Run();
