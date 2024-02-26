using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Courses;

namespace Silicon_design_webapp.Controllers;

public class CoursesController : Controller
{
    [Route("/courses")]
    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new CoursesIndexViewModel();
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }
}
