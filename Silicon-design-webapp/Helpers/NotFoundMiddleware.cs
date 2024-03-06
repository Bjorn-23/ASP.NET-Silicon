namespace Silicon_design_webapp.Helpers
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response.StatusCode == 404 || !context.Items.ContainsKey("endpoint"))
            {
                context.Response.Redirect("/error/404");
                return;
            }

            await _next.Invoke(context);
        }        
    }
}
