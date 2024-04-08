using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Courses;
using System.Diagnostics;

namespace Silicon_design_webapp.Controllers;

[Authorize]
public class CoursesController(IConfiguration configuration, HttpClient httpClient) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;

    [Route("/courses")]
    [HttpGet]
    public async Task<IActionResult> Index(string category = "", string? searchQuery = "")
    {
        var viewModel = new CoursesViewModel();

        var models = await PopulateCourses(category, searchQuery);
        if ( models.Courses != null && models.Categories != null)
        {
            viewModel.Courses = models.Courses;
            viewModel.Categories = models.Categories;
            return View(viewModel);
        }

        return View(viewModel);
    }

    [Route("/courses/details")]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var viewModel = new CourseDetailsViewModel();

        var response = await _httpClient.GetAsync($"https://localhost:7034/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CourseBoxModel>(jsonStrings);
            viewModel.Course = model!;
            return View(viewModel);
        };

        //Should probably be returned to other view with an errormessage here...
        ViewData["Title"] = viewModel.Course.Title;
        return View(viewModel);
    }

    public async Task<(IEnumerable<CourseBoxModel> Courses, IEnumerable<CategoryModel> Categories)> PopulateCourses(string category, string searchQuery)
    
    {
        try
        {
            var categoriesResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Category/?key={_configuration["ApiKey:Secret"]}");
            var courseResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Courses?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&key={_configuration["ApiKey:Secret"]}"); //?category={category}
            if (courseResponse.IsSuccessStatusCode && categoriesResponse.IsSuccessStatusCode)
            {
                var categoryStrings = await categoriesResponse.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(categoryStrings);

                var courseStrings = await courseResponse.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseBoxModel>>(courseStrings);

                if (courses != null && categories != null)
                {
                    return (courses, categories);
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return (null!,  null!);
    }
}