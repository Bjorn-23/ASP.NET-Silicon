using Business.Factories;
using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using Silicon_design_webapp.ViewModels.Admin;
using System.Net;

namespace Silicon_design_webapp.Controllers;

[Authorize(Policy = "Admin")]
public class AdminController(AdminService adminService, UserService userService) : Controller
{
    private readonly AdminService _adminService = adminService;
    private readonly UserService _userService = userService;


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

        if (searchModel != null)
        {
            var result = await _adminService.DetermineDataType(searchModel);
            if (result != null)
            {
                ModelState.Clear();
                viewModel = AddModels(result); 
                return View(viewModel);
            }           
        }

        if (passwordModel != null && TempData["UserId"] != null)
        {
            string id = TempData["UserId"]!.ToString()!;
            var user = await _userService.GetUserAsAdminAsync(id);
            var userEntity = (UserEntity)user.ContentResult!;
            var result = await _userService.UpdateUserPasswordAsync(userEntity, passwordModel);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                ModelState.Clear();
                //viewModel.PasswordUpdate = (PasswordUpdateModel)result.ContentResult!;
                viewModel.BasicInfo = UserFactory.Create(userEntity);
                return View(viewModel);
            }
        }

        if (userModel != null)
        {
            var result = await _userService.UpdateUserAsync(User, userModel);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                ModelState.Clear();
                viewModel.BasicInfo = (BasicInfoModel)result.ContentResult!;
                return View(viewModel);
            }
        }


        //TempData["ErrorMessage"] = "Search not valid";
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
}
