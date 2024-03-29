using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Courses;

namespace Silicon_design_webapp.Controllers;

[Authorize]
public class CoursesController(IConfiguration configuration) : Controller
{
    private readonly IConfiguration _configuration = configuration;

    [Route("/courses")]
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var viewModel = new CoursesViewModel();
        using var http = new HttpClient();

        var response = await http.GetAsync($"https://localhost:7034/api/Courses/?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<CourseBoxModel>>(jsonStrings);
            viewModel.Courses = models!;
            return View(viewModel);
        }

        return View(viewModel);
    }

    [Route("/courses/details")]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var viewModel = new CourseDetailsViewModel();
        using var http = new HttpClient();

        var response = await http.GetAsync($"https://localhost:7034/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CourseBoxModel>(jsonStrings);
            viewModel.Course = model!;
            return View(viewModel);
        };

        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }
}