namespace Silicon_design_webapp.Configurations;

public static class AuthenticationConfiguration
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {       
        services.AddAuthentication()
        .AddFacebook(x =>
        {
            x.AppId = configuration["Authentication:Facebook:ClientId"]!;
            x.AppSecret = configuration["Authentication:Facebook:ClientSecret"]!;
            x.Fields.Add("first_name");
            x.Fields.Add("last_name");
        })
        .AddGoogle(x =>
        {
            x.ClientId = configuration["Authentication:Google:ClientId"]!;
            x.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;

        });
    }
}
