namespace Silicon_design_webapp.Helpers;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseUserSessionValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserSessionValidationMiddleware>();
    }
}
