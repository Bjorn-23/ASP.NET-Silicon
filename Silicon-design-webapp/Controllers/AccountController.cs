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
        
    [HttpGet]
    public IActionResult Security()
    {
        var viewModel = new AccountSecurityViewModel();
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult PasswordInfo(AccountSecurityViewModel viewModel)
    {
        // _accountService.UpdatedPassword(viewModel.AccountSecurityPasswordInfoModel)
        return RedirectToAction(nameof(Security));
    }

    [HttpPost]
    public IActionResult DeleteAccount(AccountSecurityViewModel viewModel)
    {
        // _accountService.DeleteAccount(viewmodel.AccountSecurityDeleteModel)
        return RedirectToAction(nameof(Security));
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
