using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.Models;
using Silicon_design_webapp.ViewModels.Account;

namespace Silicon_design_webapp.Controllers;

public class AccountController : Controller
{
    //private readonly AccountService _accountService;

    //public AccountController(AccountService accountService)
    //{
    //    _accountService = accountService;
    //}

    [Route("/account")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        //viewModel.BasicInfo = _accountService.GetBasicInfo
        //viewModel.AddressInfo = _accountService.GetBasicInfo
        //viewModel.Sidebar = _accountService.GetBasicInfo(viewModel.BasicInfo.Email) //fetches the data needed based on email and matches that with account...might not be needed
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
    {
        //_accountservice.SaveBasicInfo(viewModel.BasicInfo)
        return RedirectToAction(nameof(Details));
    }

    [HttpPost]
    public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
    {
        //_accountservice.SaveAddresInfo(viewModel.AddressInfo)
        return RedirectToAction(nameof(Details));
    }

    [Route("/security")]
    [HttpGet]
    public IActionResult Security()
    {
        var viewModel = new AccountSecurityViewModel();
        ViewData["Title"] = "Security";
        return View(viewModel);
    }

    [Route("/update-password-failed")]
    [HttpPost]
    public IActionResult PasswordInfo(AccountSecurityPasswordInfoModel viewModel)
    {
        var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

        if(ModelState.IsValid)
        {
            // _accountService.UpdatePassword(viewModel.Forml) + other logic here.

            return RedirectToAction(nameof(Security));
        }

        var viewModelError = new AccountSecurityViewModel();
        viewModelError.ErrorMessage = "Failed to update password";
        ViewData["Title"] = "Security";
        return View("Security",  viewModelError);
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
        ViewData["Title"] = "Security";
        return View("Security", viewModelError);
    }

    [HttpGet]
    public IActionResult SavedCourses()
    {
        var viewModel = new AccountSavedCoursesViewModel();
        ViewData["Title"] = viewModel.Title;
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
