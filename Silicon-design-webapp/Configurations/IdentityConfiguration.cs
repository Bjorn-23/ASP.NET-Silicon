using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Identity;

namespace Silicon_design_webapp.Configurations;

public static class IdentityConfiguration
{
    public static void RegisterDefaultIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<UserEntity>(x =>
        {
            x.User.RequireUniqueEmail = true;
            x.SignIn.RequireConfirmedAccount = false;
            x.Password.RequiredLength = 8;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
