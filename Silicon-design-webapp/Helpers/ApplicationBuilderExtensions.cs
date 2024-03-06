namespace Silicon_design_webapp.Helpers;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseNotFoundMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<NotFoundMiddleware>();
    }

    public static IApplicationBuilder UseUserSessionValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserSessionValidationMiddleware>();
    }
}
