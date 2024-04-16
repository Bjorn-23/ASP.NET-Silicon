using Business.Factories;
using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.Helpers;
using Silicon_design_webapp.ViewModels.Admin;
using System.Net.Http.Headers;
using System.Text;

namespace Silicon_design_webapp.Controllers;

[Authorize(Policy = "Admin")]
public class AdminController(AdminService adminService, IConfiguration configuration, HttpClient httpClient, CategoryIdAssigner categoryIdAssigner) : Controller
{
    private readonly AdminService _adminService = adminService;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
    private readonly CategoryIdAssigner _categoryIdAssigner = categoryIdAssigner;

    #region USERS

    [HttpGet("/admin")]
    public async Task<IActionResult> Index()
    {
        var tokenResponse = await _httpClient.SendAsync(new HttpRequestMessage
        {
            RequestUri = new Uri($"https://localhost:7034/api/Auth?key={_configuration["ApiKey:Secret"]}"),
            Method = HttpMethod.Post,
        });
        if (tokenResponse.IsSuccessStatusCode)
        {
            var token = await tokenResponse.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("token", token);
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));


        var viewModel = new AdminViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(AdminSearchModel? searchModel, BasicInfoModel? userModel, AddressInfoModel? addressModel, PasswordUpdateModel passwordModel)
    {
        var viewModel = new AdminViewModel();

        if (searchModel != null && !string.IsNullOrWhiteSpace(searchModel.Expression))
        {
            var result = await _adminService.SearchDataByStringInput(searchModel);
            if (result != null)
            {
                ModelState.Clear();
                viewModel = ConvertToModel(result);
                return View(viewModel);
            }
            else
            {
                ViewData["ErrorMessage"] = "No data found";
                return View(viewModel);
            }
        }

        if (passwordModel != null && TempData["UserId"] != null)
        {
            string id = TempData["UserId"]!.ToString()!;
            var user = await _adminService.GetOneUserByIdAsync(id);
            var userEntity = (UserEntity)user.Content!;
            var result = await _adminService.UpdateUserPasswordAsync(userEntity, passwordModel);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                ModelState.Clear();
                viewModel.BasicInfo = UserFactory.Create(userEntity);
                return View(viewModel);
            }
            else
            {
                ModelState.Clear();
                viewModel.BasicInfo = UserFactory.Create(userEntity);
                ViewData["ErrorMessage"] = "Failed to update users password";
                return View(viewModel);
            }
        }

        if (userModel != null && userModel.Email != null) // Since isExternal is by default set to false, null check for Email makes sure that an empty search trigger this conditional statement. 
        {
            var result = await _adminService.UpdateUserAsync(userModel);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                ModelState.Clear();
                viewModel.BasicInfo = (BasicInfoModel)result.Content!;
                return View(viewModel);
            }
            else
            {
                ViewData["ErrorMessage"] = "Failed to update user information";
                return View(viewModel);
            }
        }

        ViewData["ErrorMessage"] = "Search not valid";
        return View(viewModel);
    }

    [HttpGet("/admin/userinfo")]
    public IActionResult UserInfo(Object user)
    {
        if (ModelState.IsValid && user != null)
        {
            return View(user);
        }
        return RedirectToAction("Index");
    }

    [HttpGet("/admin/addressinfo")]
    public IActionResult AddressInfo(AddressInfoModel address)
    {
        if (ModelState.IsValid && address != null)
        {
            return View(address);
        }

        return RedirectToAction("Index");
    }

    [HttpGet("/admin/passwordinfo")]
    public IActionResult PasswordInfo()
    {
        return RedirectToAction("Index");
    }

    [HttpGet("/admin/deleteaccount")]
    public IActionResult DeleteAccount()
    {
        return RedirectToAction("Index");
    }

    private AdminViewModel ConvertToModel(Object result)
    {
        var viewModel = new AdminViewModel();
        // Assigns the value of 'result' to a variable of matching type within this case block.
        switch (result)
        {
            case IEnumerable<BasicInfoModel> basicInfoEnumerable:
                viewModel.Users = basicInfoEnumerable;
                break;
            case IEnumerable<AddressInfoModel> addressInfoEnumerable:
                viewModel.Addresses = addressInfoEnumerable;
                break;
            case BasicInfoModel basicInfo:
                viewModel.BasicInfo = basicInfo;
                break;
            case AddressInfoModel addressInfo:
                viewModel.AddressInfo = addressInfo;
                break;
            case PasswordUpdateModel passwordUpdate:
                viewModel.PasswordUpdate = passwordUpdate;
                break;
            case DeleteAccountModel deleteAccount:
                viewModel.DeleteAccount = deleteAccount;
                break;
            default:
                break;
        }

        return viewModel;

    }

    #endregion

    #region SUBSCRIBERS  

    #region CREATE

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(AdminSubscriptionViewModel viewModel)
    {
        using var http = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(viewModel.Subscriber), Encoding.UTF8, "application/json");

        var response = await http.PostAsync($"https://localhost:7034/api/Subscriptions?key={_configuration["ApiKey:Secret"]}", content);
        if (response.IsSuccessStatusCode)
        {
            TempData["SubscriptionStatus"] = "Subscription created succesfully";
            return RedirectToAction("Subscription");
        }

        TempData["SubscriptionStatus"] = "Subscription created succesfully";
        return RedirectToAction("Subscribe");
    }

    #endregion

    #region READ

    [HttpGet("/admin/subscriptions")]
    public async Task<IActionResult> Subscription()
    {
        var viewModel = new AdminSubscriptionViewModel();

        var response = await _httpClient.GetAsync($"https://localhost:7034/api/Subscriptions?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<AdminSubscribeModel>>(jsonStrings);
            viewModel.Subscribers = models!;
            return View(viewModel);
        }

        ViewData["SubscriptionStatus"] = "No subscribers in list";
        return View(viewModel);
    }


    [HttpGet("/admin/subscriptions/{Id}")]
    public async Task<IActionResult> Subscription(string Id)
    {
        var viewModel = new AdminSubscriptionViewModel();
        using var http = new HttpClient();

        var response = await http.GetAsync($"https://localhost:7034/api/Subscriptions/{Id}?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<AdminSubscribeModel>(jsonStrings);
            if (model != null)
            {
                List<AdminSubscribeModel> models = [];
                models.Add(model);
                viewModel.Subscribers = models!;
                return View(viewModel);
            }
        }

        ViewData["SubscriptionStatus"] = "No subscribers in list";
        return View(viewModel);
    }

    #endregion

    #region UPDATE

    [HttpPost]
    public async Task<IActionResult> UpdateSubscription(AdminSubscriptionViewModel viewModel)
    {
        using var http = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(viewModel.Subscriber), Encoding.UTF8, "application/json");

        var response = await http.PutAsync($"https://localhost:7034/api/Subscriptions?key={_configuration["ApiKey:Secret"]}", content);
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<AdminSubscribeModel>(jsonStrings);
            if (model != null)
            {
                TempData["SubscriptionStatus"] = "Subscription updated succesfully";
                return RedirectToAction("Subscription");
                //List<AdminSubscribeModel> models = [];
                //models.Add(model);
                //viewModel.Subscribers = models!;
                //return View(viewModel);
            }
        }

        TempData["SubscriptionStatus"] = "Failed to update subscription";
        return RedirectToAction("Subscription");
    }

    #endregion

    #region DELETE

    [HttpPost("/admin/subscriptions/delete")]
    public async Task<IActionResult> DeleteSubscription(AdminSubscriptionViewModel viewModel)
    {
        using var http = new HttpClient();
        var Id = viewModel.Subscriber.Id;

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var response = await http.DeleteAsync($"https://localhost:7034/api/Subscriptions/{Id}?key={_configuration["ApiKey:Secret"]}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            TempData["SubscriptionStatus"] = "Subscription deleted succesfully";
            return RedirectToAction("Subscription");
        }

        TempData["SubscriptionStatus"] = "Failed to delete subscription";
        return RedirectToAction("Subscription");

    }

    #endregion

    #endregion

    #region COURSES

    [HttpGet("/admin/courses")]
    public async Task<IActionResult> Courses(string category = "", string searchQuery = "")
    {
        var viewModel = new AdminCoursesViewModel();

        var categoriesResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Category?key={_configuration["ApiKey:Secret"]}");
        if (categoriesResponse.IsSuccessStatusCode)
        {
            var jsonStrings = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(jsonStrings);
            if (categories != null)
            {
                viewModel.Categories = categories!;
            }
        }

        var response = await _httpClient.GetAsync($"https://localhost:7034/api/Courses?category={Uri.EscapeDataString(category)}&searchQuery={Uri.EscapeDataString(searchQuery)}&key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<CourseResult>(jsonStrings);
            if (courses != null)
            {
                viewModel.Courses = courses!.ReturnCourses;
                return View(viewModel);
            }
        }

        ViewData["CourseStatus"] = response.StatusCode;
        return View(viewModel);   
    }

    [HttpGet("/admin/courses/{Id}")]
    public async Task<IActionResult> Courses(string Id)
    {
        var viewModel = new AdminCoursesViewModel();

        var response = await _httpClient.GetAsync($"https://localhost:7034/api/Courses/{Id}?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonStrings = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CourseBoxModel>(jsonStrings);
            viewModel.Course = model!;
            return View(viewModel);
        }
        else
        {
            ViewData["CourseStatus"] = response.StatusCode;
            return View(viewModel);
        }
    }

    [HttpPost("/admin/courses/update")]
    public async Task<IActionResult> UpdateCourse(AdminCoursesViewModel viewModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

        var categoryResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Category/{viewModel.Course.Category}?key={_configuration["ApiKey:Secret"]}");
        if (categoryResponse.IsSuccessStatusCode)
        {
            var category = await _categoryIdAssigner.assignIdAsync(categoryResponse);
            viewModel.Course.CategoryId = category.Id;
            viewModel.Course.Category = category.CategoryName;
        }

        var content = new StringContent(JsonConvert.SerializeObject(viewModel.Course), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"https://localhost:7034/api/Courses?key={_configuration["ApiKey:Secret"]}", content);
        if (response.IsSuccessStatusCode)
        {
            ViewData["CourseStatus"] = "Course updated succesfully";
            return RedirectToAction("Courses");
        }
        else
        {
            ViewData["CourseStatus"] = response.StatusCode;
            return RedirectToAction("Courses");
        }
    }


    [HttpPost("/admin/courses/create")]
    public async Task<IActionResult> CreateCourse(AdminCoursesViewModel viewModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

        var categoryResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Category/{viewModel.CreateCourse.Category}?key={_configuration["ApiKey:Secret"]}");
        if (categoryResponse.IsSuccessStatusCode)
        {
            var category = await _categoryIdAssigner.assignIdAsync(categoryResponse);
            viewModel.CreateCourse.CategoryId = category.Id;
            viewModel.CreateCourse.Category = category.CategoryName;
        }

        var content = new StringContent(JsonConvert.SerializeObject(viewModel.CreateCourse), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"https://localhost:7034/api/Courses?key={_configuration["ApiKey:Secret"]}", content);
        if (response.IsSuccessStatusCode)
        {
            ViewData["CourseStatus"] = "Course created succesfully";
            return RedirectToAction("Courses");
        }
        else
        {
            ViewData["CourseStatus"] = response.StatusCode;
            return RedirectToAction("Courses");
        }
    }


    [HttpPost("/admin/courses/delete")]
    public async Task<IActionResult> DeleteCourse(AdminCoursesViewModel viewModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

        var Id = viewModel.Course.Id;
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var response = await _httpClient.DeleteAsync($"https://localhost:7034/api/Courses/{Id}?key={_configuration["ApiKey:Secret"]}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            ViewData["CourseStatus"] = "Course deleted succesfully";
            return RedirectToAction("Courses");
        }
        else
        {
            ViewData["CourseStatus"] = response.StatusCode;
            return RedirectToAction("Courses");
        }
    }

    #endregion

    #region CONTACT

    [HttpGet("/admin/contact")]
    public async Task<IActionResult> Contact()
    {
        var viewModel = new AdminContactViewModel();
        using var http = new HttpClient();

        ViewData["StatusMessage"] = TempData["StatusMessage"] ?? "";

        var response = await http.GetAsync($"https://localhost:7034/api/Contact?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<AdminContactModel>>(jsonString);
            viewModel.ContactForms = models!;
            return View(viewModel);
        }
        else
        {
            ViewData["ErrorMessage"] = response.StatusCode.ToString();
            return View(viewModel);
        }
    }

    [HttpGet("/admin/contactdetails/{Id}")]
    public async Task<IActionResult> ContactDetails(string Id)
    {
        var viewModel = new AdminContactViewModel();
        using var http = new HttpClient();

        ViewData["StatusMessage"] = TempData["StatusMessage"] ?? "";

        var response = await http.GetAsync($"https://localhost:7034/api/Contact/{Id}?key={_configuration["ApiKey:Secret"]}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<AdminContactModel>(jsonString);
            viewModel.Contact = model!;
            return View(viewModel);
        }
        else
        {
            ViewData["ErrorMessage"] = response.StatusCode.ToString();
            return View(viewModel);
        }
    }

    [HttpPost("/admin/contact/update")]
    public async Task<IActionResult> UpdateContact(AdminContactViewModel viewModel)
    {

        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(viewModel.Contact), Encoding.UTF8, "application/json");

            var response = await http.PutAsync($"https://localhost:7034/api/Contact?key={_configuration["ApiKey:Secret"]}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["StatusMessage"] = "Success - Contact updated";
                return RedirectToAction("Contact");
            }

            TempData["StatusMessage"] = response.StatusCode.ToString();
            return RedirectToAction("Contact");
        }

        ViewData["ErrorMessage"] = BadRequest();
        return RedirectToAction("Contact");
    }


    [HttpPost("/admin/contact/delete/")]
    public async Task<IActionResult> DeleteContact(AdminContactViewModel viewModel)
    {
        using var http = new HttpClient();
        string id = viewModel.Contact.Id;

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var response = await http.DeleteAsync($"https://localhost:7034/api/Contact/{id}?key={_configuration["ApiKey:Secret"]}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            TempData["StatusMessage"] = "Success - Contact deleted";
            return RedirectToAction("Contact");
        }

        TempData["StatusMessage"] = response.StatusCode.ToString();
        return RedirectToAction("Contact");
    }


    #endregion

}
