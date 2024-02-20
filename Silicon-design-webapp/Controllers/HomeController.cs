using Microsoft.AspNetCore.Mvc;

namespace Silicon_design_webapp.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
