using Business.Models;
using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using Silicon_design_webapp.ViewModels.Admin;
using System.Net;

namespace Silicon_design_webapp.Controllers;

[Authorize(Policy ="Admin")]
public class AdminController(AdminService adminService) : Controller
{
    private readonly AdminService _adminService = adminService;

    [Route("/admin")]
    [HttpGet]
    public IActionResult Index()
    {

        var viewModel = new AdminViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(AdminSearchModel model)
    {
        if (ModelState.IsValid)
        {
            var viewModel = new AdminViewModel();

            var result = await _adminService.DetermineDataType(model);
            if (result != null)
            {
                if (result is IEnumerable<BasicInfoModel>)
                {
                    viewModel.Users = (IEnumerable<BasicInfoModel>)result;
                }
                else if (result is IEnumerable<AddressInfoModel>)
                {
                    viewModel.Addresses = (IEnumerable<AddressInfoModel>)result;
                }
   
                return View(viewModel);           
            }
        }

        TempData["ErrorMessage"] = "Search not valid";
        return RedirectToAction("Index");
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
}
