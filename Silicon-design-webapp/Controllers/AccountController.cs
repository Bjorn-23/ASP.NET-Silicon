using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Account;

namespace Silicon_design_webapp.Controllers;

[Authorize]
public class AccountController(SignInManager<UserEntity> signInManager, UserService userService, AddressService addressService) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserService _userService = userService;
    private readonly AddressService _addressService = addressService;

    #region Details
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
        if (activeUser.ContentResult != null)
        {
            viewModel.BasicForm = (BasicInfoModel)activeUser.ContentResult;
            viewModel.Sidebar.AccountInfo = viewModel.BasicForm;
        }

        var UserAddress = await _addressService.GetUserAddressAsync(User);
        if (UserAddress.ContentResult != null)
            viewModel.AddressForm = (AddressInfoModel)UserAddress.ContentResult;

        return View(viewModel);
    }

    #region Basic Form
    [HttpPost]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {
        var result = await _userService.UpdateUserAsync(User, viewModel.BasicForm);
        if (result != null)
            return RedirectToAction(nameof(Details));

        return RedirectToAction("Details");
    }
    #endregion

    #region AddressForm
    [HttpPost]
    public async Task<IActionResult> AddressInfo(AccountDetailsViewModel viewModel)
    {
        var result = await _addressService.GetOrCreateAddressAsync(User, viewModel.AddressForm);
        if (result != null)
            return RedirectToAction(nameof(Details));
        return RedirectToAction(nameof(Details));
    }
    #endregion
    #endregion

    #region Security
    [Route("/security")]
    [HttpGet]
    public async Task<IActionResult> Security()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }
        var viewModel = new AccountSecurityViewModel();

        var activeUser = await _userService.GetActiveUserAsync(User);
        if (activeUser.ContentResult != null)
        {
            viewModel.Sidebar.AccountInfo = (BasicInfoModel)activeUser.ContentResult;
        }
        return View(viewModel);
    }

    #region Update Password
    [Route("/security/update-password")]
    [HttpPost]
    public async Task<IActionResult> UpdatePassword(PasswordUpdateModel viewModel)
    {
        ///Checks for errors in the ModelState, handy for debugging.
        //var errors = ModelState
        //    .Where(x => x.Value!.Errors.Count > 0)
        //    .Select(x => new { x.Key, x.Value!.Errors })
        //    .ToArray();

        if (ModelState.IsValid)
        {
            var result = await _userService.UpdateUserPasswordAsync(User, viewModel);

            var viewModelResult = new AccountSecurityViewModel();
            viewModelResult.StatusMessage = result.Message;
            return View("Security", viewModelResult);
        }

        var viewModelError = new AccountSecurityViewModel();
        viewModelError.StatusMessage = "Failed to update password";
        return View("Security", viewModelError);
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
        return View("Security", viewModelError);
    }
    #endregion
    #endregion

    #region Courses
    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }
        var viewModel = new AccountSavedCoursesViewModel();

        var activeUser = await _userService.GetActiveUserAsync(User);
        if (activeUser.ContentResult != null)
        {
            viewModel.Sidebar.AccountInfo = (BasicInfoModel)activeUser.ContentResult;
        }
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
    #endregion
}
