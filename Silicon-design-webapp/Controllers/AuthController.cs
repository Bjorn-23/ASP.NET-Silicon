using Microsoft.AspNetCore.Mvc;

namespace Silicon_design_webapp.Controllers;

public class AuthController : Controller
{
    [Route("/SignUp")]
    public IActionResult SignUp()
    {
        return View();
    }
}
