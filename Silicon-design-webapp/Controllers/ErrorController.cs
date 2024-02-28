using Microsoft.AspNetCore.Mvc;

namespace Silicon_design_webapp.Controllers;

public class ErrorController : Controller
{
    [HttpGet("/error/404")]
    public IActionResult Error()
    {
        return View();
    }
}
