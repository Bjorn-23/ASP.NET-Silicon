using Infrastructure.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Silicon_design_webapp.Helpers;

public class UserSessionValidationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static bool IsAjaxRequest(HttpRequest request) => request.Headers.XRequestedWith == "XMLHttpRequest";

    public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            //checks if a user has been deleted but is still flagged as being logged in, if so logs the User out.
            if (!await userManager.Users.AnyAsync(x => x.UserName == context.User.Identity.Name))
            {
                await signInManager.SignOutAsync(); 
            }

            var user = await userManager.GetUserAsync(context.User);
            if (user == null)
            {
                await signInManager.SignOutAsync();

                if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    var signInPath = "/signin";
                    context.Response.Redirect(signInPath);
                    return;
                }
            }
        }

        await _next.Invoke(context);
    }    
}
