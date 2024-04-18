namespace Silicon_design_webapp.Configurations;

public static class SessionConfiguration
{
    public static void RegisterSession(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSession(x =>
        {
            x.IdleTimeout = TimeSpan.FromMinutes(20);
            x.Cookie.IsEssential = true;
            x.Cookie.HttpOnly = true;
        });
    }
}