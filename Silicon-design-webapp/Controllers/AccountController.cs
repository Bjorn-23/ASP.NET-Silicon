using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Account;
using System.Text.RegularExpressions;

namespace Silicon_design_webapp.Controllers;

[Authorize(Policy = "User")]
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
    public async Task<IActionResult> Security()
    {
        var viewModel = new AccountSecurityViewModel();

        var activeUser = await _userService.GetActiveUserAsync(User);
        if (activeUser.ContentResult != null)
        {
            viewModel.Sidebar.AccountInfo = (BasicInfoModel)activeUser.ContentResult;
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
                return View("Security", accountViewModel);
            }
        }

        ViewBag.isExternalAccount = user?.IsExternalAccount;
        accountViewModel.StatusMessage = "Failed to update password";
        return View("Security", accountViewModel);
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
    [Route("/savedcourses")]
    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
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

