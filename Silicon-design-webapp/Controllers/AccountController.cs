using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Account;

namespace Silicon_design_webapp.Controllers;

[Authorize]
public class AccountController(SignInManager<UserEntity> signInManager, UserService userService) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserService _userService = userService;

    [Route("/account")]
    [HttpGet]
    public async Task<IActionResult> Details()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var viewModel = new AccountDetailsViewModel();
        
        var activeUser = await _userService.GetActiveUserAsync(User);
        if(activeUser.ContentResult != null)
            viewModel.BasicForm = (AccountDetailsBasicInfoModel)activeUser.ContentResult;

        var UserAddress = await _userService.GetUserAddressAsync(User);
        if (UserAddress.ContentResult != null)
            viewModel.AddressForm = (AccountDetailsAddressInfoModel)UserAddress.ContentResult;
            
        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {
        var result = await _userService.UpdateUserAsync(User, viewModel.BasicForm);
        if (result != null)
            return RedirectToAction(nameof(Details));

        return RedirectToAction("Details");
    }


    [HttpPost]
    public async Task<IActionResult> AddressInfo(AccountDetailsViewModel viewModel)
    {
        var result = await _userService.CreateOrUpdateAddressAsync(User, viewModel.AddressForm);
        if (result != null)
            return RedirectToAction(nameof(Details));
        return RedirectToAction(nameof(Details));
    }


    [Route("/security")]
    [HttpGet]
    public IActionResult Security()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var viewModel = new AccountSecurityViewModel();
        return View(viewModel);
    }


    [Route("/update-password-failed")]
    [HttpPost]
    public IActionResult PasswordInfo(AccountSecurityPasswordInfoModel viewModel)
    {
        ///Checks for errors in the ModelState, handy for debugging.
        var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

        if (ModelState.IsValid)
        {
            // _accountService.UpdatePassword(viewModel.Forml) + other logic here.

            return RedirectToAction(nameof(Security));
        }

        var viewModelError = new AccountSecurityViewModel();
        viewModelError.ErrorMessage = "Failed to update password";
        return View("Security", viewModelError);
    }


    [Route("/delete-account-failed")]
    [HttpPost]
    public IActionResult DeleteAccount(AccountSecurityDeleteModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // _accountService.UpdatePassword(viewModel.Forml) + other logic here.
            return RedirectToAction(nameof(Security));
        }

        var viewModelError = new AccountSecurityViewModel();
        viewModelError.ErrorMessage = "Account could not be deleted";
        return View("Security", viewModelError);
    }


    [HttpGet]
    public IActionResult SavedCourses()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var viewModel = new AccountSavedCoursesViewModel();
        return View(viewModel);
    }

    // How do I pick just one item to delete?
    [HttpPost]
    public IActionResult DeleteBookmarkedCourse(AccountSavedCoursesViewModel viewModel)
    {
        // _accountService.UpdateSavedCourses(viewModel.Courses) // set one bookmark to false?
        return RedirectToAction(nameof(SavedCourses));
    }

    [HttpPost]
    public IActionResult DeleteAllSavedCourses(AccountSavedCoursesViewModel viewModel)
    {
        // _accountService.UpdateSavedCourses(viewModel.Courses) // set all bookmarks to false?
        return RedirectToAction(nameof(SavedCourses));
    }
}
