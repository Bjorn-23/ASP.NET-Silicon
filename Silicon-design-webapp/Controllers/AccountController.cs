using Business.Factories;
using Business.Models;
using Business.Services;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Account;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Silicon_design_webapp.Controllers;

[Authorize(Policy = "User")]
public class AccountController(SignInManager<UserEntity> signInManager, UserService userService, AddressService addressService, HttpClient httpClient, IConfiguration configuration, ApplicationDbContext context) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserService _userService = userService;
    private readonly AddressService _addressService = addressService;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly ApplicationDbContext _context = context;


    #region Details
    [Route("/account")]
    [HttpGet]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel();

        if (ModelState.IsValid)
        {
            var activeUser = await _userService.GetActiveUserAsync(User);
            if (activeUser.Content != null)
            {
                viewModel.BasicForm = (BasicInfoModel)activeUser.Content;
                viewModel.Sidebar.AccountInfo = viewModel.BasicForm;
            }

            var UserAddress = await _addressService.GetUserAddressAsync(User);
            if (UserAddress.Content != null)
                viewModel.AddressForm = (AddressInfoModel)UserAddress.Content;
        }
        
        return View(viewModel);
    }

    #region Basic Form
    [HttpPost]
    public async Task<IActionResult> BasicInfo(BasicInfoModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.UpdateUserAsync(User, model);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
                return RedirectToAction("Details");
        }

        //If Modelstate is not valid I get the view again and populate with valid details.
        return RedirectToAction("Details");
    }
    #endregion


    #region AddressForm
    [HttpPost]
    public async Task<IActionResult> AddressInfo(AddressInfoModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _addressService.GetOrCreateAddressAsync(User, model);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
                return RedirectToAction("Details");
        }

        return RedirectToAction("Details");
    }
    #endregion
    #endregion

    #region Security
    [Route("/security")]
    [HttpGet]
    public async Task<IActionResult> Security(AccountSecurityViewModel? returnViewmodel)
    {
        var viewModel = returnViewmodel ?? new AccountSecurityViewModel();

        var activeUser = await _userService.GetActiveUserAsync(User);
        if (activeUser.Content != null)
        {
            viewModel.Sidebar.AccountInfo = (BasicInfoModel)activeUser.Content;
            ViewBag.isExternalAccount = viewModel.Sidebar.AccountInfo.IsExternalAccount;
        }
        return View(viewModel);
    }

    #region Update Password
    [Route("/security/update-password")]
    [HttpPost]
    public async Task<IActionResult> UpdatePassword(PasswordUpdateModel viewModel)
    {
        var user = await _signInManager.UserManager.GetUserAsync(User);
        var accountViewModel = new AccountSecurityViewModel();

        // checks either for ModelState.IsValid or that the new password set for an external User lives up to the regexp validation requirements and match with confirmNewPassword.
        if (ModelState.IsValid ||
            user != null
            && user.IsExternalAccount
            && string.IsNullOrEmpty(user.PasswordHash)
            && viewModel.NewPassword == viewModel.ConfirmNewPassword
            && Regex.IsMatch(viewModel.NewPassword, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$"))
        {
            var result = await _userService.UpdateUserPasswordAsync(User, viewModel);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                ModelState.Clear();
                accountViewModel.StatusMessage = result.Message;
                return RedirectToAction("Security", accountViewModel);
            }
        }

        ViewBag.isExternalAccount = user?.IsExternalAccount;
        accountViewModel.StatusMessage = "Failed to update password";
        return RedirectToAction("Security", accountViewModel);
    }
    #endregion

    #region Delete account
    [Route("/security/delete-account")]
    [HttpPost]
    public async Task<IActionResult> DeleteAccount(DeleteAccountModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.DeleteUserAccount(User);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
        }

        var viewModelError = new AccountSecurityViewModel();
        viewModelError.StatusMessage = "Account could not be deleted";
        ModelState.Clear();
        return RedirectToAction("Security", viewModelError);
    }
    #endregion
    #endregion

    #region Courses
    [Route("/savedcourses")]
    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
        var viewModel = new AccountSavedCoursesViewModel();

        var courseResponse = await _httpClient.GetAsync($"https://localhost:7034/api/Courses?key={_configuration["ApiKey:Secret"]}");
        if (courseResponse.IsSuccessStatusCode)
        {
            var courseStrings = await courseResponse.Content.ReadAsStringAsync();
            var courseResult = JsonConvert.DeserializeObject<CourseResult>(courseStrings);
            if (courseResult != null)
            {
                viewModel.Courses = courseResult.ReturnCourses;
            }
        }
        var savedCourses = await _userService.GetSavedCourses(User);
        if (savedCourses != null)
        {
            viewModel.SavedCourses = savedCourses;
        }

        var activeUser = await _userService.GetActiveUserAsync(User);
        if (activeUser.Content != null)
        {
            viewModel.Sidebar.AccountInfo = (BasicInfoModel)activeUser.Content;
        }

        ViewData["CourseStatus"] = TempData["CourseStatus"] ?? "";
        return View(viewModel);
    }

    [Route("/account/bookmarkcourse{courseId}")]
    [HttpPost]
    public async Task<IActionResult> BookmarkCourse(string courseId)
    {
        //var tracked = _context.ChangeTracker.Entries<SavedCoursesEntity>().Any(e => e.Entity.UserId == savedCourse.UserId && e.Entity.CourseId == savedCourse.CourseId);

        var existingSavedCourse = await _userService.GetOneSavedCourse(User, courseId);
        if (existingSavedCourse == null)
        {
            var savedCourse = await _userService.CreateSavedCourse(User, courseId);
            if (savedCourse)
            {
                TempData["CourseStatus"] = "Course saved Successfully.";
                return RedirectToAction("SavedCourses");
            }
        }
        else
        {
            var result = await _userService.DeleteSavedCourse(existingSavedCourse);
            if (result)
            {
                TempData["CourseStatus"] = "Course deleted.";
                return RedirectToAction("Index", "Courses");
            }
        }

        TempData["CourseStatus"] = "Something went wrong, contact site owner if issue persists.";
        return RedirectToAction("SavedCourses");
    }

    //[Route("/account/deleteAllSavedCourses")]
    [HttpPost]
    public async Task<IActionResult> DeleteAllSavedCourses()
    {
            var result = await _userService.DeleteAllSavedCourses(User);
            if (result)
            {
                TempData["CourseStatus"] = "Courses deleted.";
                return RedirectToAction("Index", "Courses");
            }

        TempData["CourseStatus"] = "Something went wrong, contact site owner if issue persists.";
        return RedirectToAction("SavedCourses");
    }



    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var result = await _userService.UploadUserProfileImageAsync(User, file);
        return RedirectToAction("index");
    }
    #endregion
}

