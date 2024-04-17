using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Courses;
using System.Diagnostics;

namespace Silicon_design_webapp.Controllers;

[Authorize]
public class CoursesController(IConfiguration configuration, HttpClient httpClient, UserService userService) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
    private readonly UserService _userService = userService;

    [Route("/courses")]
    [HttpGet]
    public async Task<IActionResult> Index(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        var viewModel = new CoursesViewModel();

        var models = await PopulateCourses(category, searchQuery, pageNumber, pageSize);
        if (models.Courses != null && models.Categories != null)
        {
            viewModel.CategoryQuery = category;
            viewModel.SavedCourses = models.SavedCourses;
            viewModel.Courses = models.Courses;
            viewModel.Categories = models.Categories;
            viewModel.Pagination = models.Pagination;
            return View(viewModel);
        }

        ViewData["StatusMessage"] = "Something went wrong, please contact site owner if issue persists.";
        return View(viewModel);
    }

    [Route("/courses/details")]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var viewModel = new CourseDetailsViewModel();

        var savedCourses = await _userService.GetSavedCourses(User);
        var response = await _httpClient.GetAsync($"https://localhost:7034/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CourseBoxModel>(jsonStrings);
            viewModel.Course = model!;
            viewModel.SavedCourses = savedCourses;
            return View(viewModel);
        };

        //Should probably be returned to other view with an errormessage here...
        ViewData["Title"] = viewModel.Course.Title;
        return View(viewModel);
    }


    /// <summary>
    /// Populates the CoursesViewModel by getting each Model from the Web Api.
    /// </summary>
    /// <param name="category">Lets the Api query with lamba expression which category should be fetched from DB, defaults to empty string which gets all (logic is in Api)</param>
    /// <param name="searchQuery">Lets Api query with .Contains(searchQuery) to return matches from DB</param>
    /// <param name="pageNumber">The Current page number</param>
    /// <param name="pageSize">The number of items displayed on a page</param>
    /// <returns></returns>
    public async Task<(IEnumerable<CourseBoxModel> Courses, Pagination Pagination, IEnumerable<CategoryModel> Categories, IEnumerable<SavedCoursesModel> SavedCourses)> PopulateCourses(string category, string searchQuery, int pageNumber, int pageSize)
    {
        try
        {
            var savedCourses = await _userService.GetSavedCourses(User);

            var categories = new List<CategoryModel>();
            var categoriesResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Category/?key={_configuration["ApiKey:Secret"]}");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryModel>>(await categoriesResponse.Content.ReadAsStringAsync())!;
            }

            var courseResult = new CourseResult();
            var courseResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Courses?category={Uri.EscapeDataString(category)}&searchQuery={Uri.EscapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}&key={_configuration["ApiKey:Secret"]}"); //&pageNumber={pageNumber}&pageSize={pageSize}
            if (courseResponse.IsSuccessStatusCode)
            {
                var courseStrings = await courseResponse.Content.ReadAsStringAsync();
                courseResult = JsonConvert.DeserializeObject<CourseResult>(courseStrings)!;
            }
                        
            return (courseResult.ReturnCourses, courseResult.Pagination, categories, savedCourses);
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return (null!, null!, null!, null!);
    }
}