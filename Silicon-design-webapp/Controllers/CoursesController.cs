using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Courses;

namespace Silicon_design_webapp.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly string _apiKey = "YmRhNGYwZDgtNDNkZi00N2EyLTliNmQtODYxZTA3OTQ3NDUy";

    [Route("/courses")]
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var viewModel = new CoursesViewModel();
        var http = new HttpClient();

        var response = await http.GetAsync($"https://localhost:7034/api/Courses/?key={_apiKey}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<CourseBoxModel>>(jsonStrings);
            viewModel.Courses = models!;
            return View(viewModel);
        }

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