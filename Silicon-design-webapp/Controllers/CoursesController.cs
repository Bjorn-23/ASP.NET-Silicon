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
        return View(viewModel);
    }

    [Route("/course-details")]
    [HttpGet]
    public IActionResult Details(/*string id*/)
    {
        //var course = _coursesService.get(id);
        //ViewData["Title"] = course.Title;
        //return View(course);

        var viewModel = new CourseDetailsViewModel();
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }
}