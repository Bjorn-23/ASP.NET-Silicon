using Business.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silicon_design_webapp.Configurations;
using Silicon_design_webapp.Helpers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.RegisterDefaultIdentity(builder.Configuration);
builder.Services.RegisterApplicationCookie(builder.Configuration);
builder.Services.RegisterAuthorization(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.RegisterSession(builder.Configuration);

builder.Services.AddScoped<CategoryIdAssigner>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<AdminService>();


//--------------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------------


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
