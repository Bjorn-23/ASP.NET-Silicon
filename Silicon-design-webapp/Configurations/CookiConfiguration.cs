namespace Silicon_design_webapp.Configurations;

public static class CookiConfiguration
{
    public static void RegisterApplicationCookie(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureApplicationCookie(x =>
        {
            x.LoginPath = "/signin";
            x.LogoutPath = "/signout";
            x.AccessDeniedPath = "/denied";

            x.Cookie.HttpOnly = true;
            x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            x.SlidingExpiration = true;

        });
    }
}
