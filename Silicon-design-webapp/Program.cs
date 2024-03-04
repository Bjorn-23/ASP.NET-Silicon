using Business.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Silicon_design_webapp.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();


//add services here such as repositories, services, dbcontext etc.
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddAuthentication("AuthCookie").AddCookie("AuthCookie", x =>
{
    x.LoginPath = "/signin";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});


builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddresRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();


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
app.UseNotFoundMiddleware();

app.Run();
