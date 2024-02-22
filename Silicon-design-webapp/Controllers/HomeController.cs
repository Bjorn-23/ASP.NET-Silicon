using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels;

namespace Silicon_design_webapp.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();

        ViewData["Title"] = viewModel.Title;

        return View(viewModel);
    }
}
