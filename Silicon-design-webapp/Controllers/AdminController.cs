using Business.Factories;
using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.Helpers;
using Silicon_design_webapp.ViewModels.Admin;

namespace Silicon_design_webapp.Controllers;

[Authorize(Policy = "Admin")]
public class AdminController(AdminService adminService) : Controller
{
    private readonly AdminService _adminService = adminService;

    #region Users
    [Route("/admin")]
    [HttpGet]
    public IActionResult Index()
    {
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
                viewModel = AddModels(result);
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
            var userEntity = (UserEntity)user.ContentResult!;
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
                viewModel.BasicInfo = (BasicInfoModel)result.ContentResult!;
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

    [Route("/admin/userinfo")]
    [HttpGet]
    public IActionResult UserInfo(Object user)
    {
        if (ModelState.IsValid && user != null)
        {
            return View(user);
        }
        return RedirectToAction("Index");
    }

    [Route("/admin/addressinfo")]
    [HttpGet]
    public IActionResult AddressInfo(AddressInfoModel address)
    {
        if (ModelState.IsValid && address != null)
        {
            return View(address);
        }


        return RedirectToAction("Index");
    }

    [Route("/admin/passwordinfo")]
    [HttpGet]
    public IActionResult PasswordInfo()
    {
        return RedirectToAction("Index");
    }

    [Route("/admin/deleteaccount")]
    [HttpGet]
    public IActionResult DeleteAccount()
    {
        return RedirectToAction("Index");
    }

    private AdminViewModel AddModels(Object result)
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

    #region Subscribers        
    [HttpGet]
    public async Task<IActionResult> Subscription()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7034/api/Subscriptions/GetAll");

        var jsonStrings = await response.Content.ReadAsStringAsync();
        var models = JsonConvert.DeserializeObject<IEnumerable<AdminSubscribeModel>>(jsonStrings);

        var viewModel = new AdminSubscriptionViewModel();
        viewModel.Subscribers = models!;

        return View(viewModel);
    }
    #endregion

}
