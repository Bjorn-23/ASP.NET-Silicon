namespace Silicon_design_webapp.Configurations;

public static class AuthorizationConfiguration
{
    public static void RegisterAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(x =>
        {
            x.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
            x.AddPolicy("CIO", policy => policy.RequireRole("SuperAdmin", "CIO"));
            x.AddPolicy("Admin", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin"));
            x.AddPolicy("Manager", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "Manager"));
            x.AddPolicy("User", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "Manager", "User"));
        });
    }
}
