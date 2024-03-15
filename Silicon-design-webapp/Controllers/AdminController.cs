using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Silicon_design_webapp.Controllers;

[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    [Route("/admin")]
    public IActionResult Index()
    {
        return View();
    }
}
