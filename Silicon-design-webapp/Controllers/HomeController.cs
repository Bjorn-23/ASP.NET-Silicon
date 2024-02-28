using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Home;

namespace Silicon_design_webapp.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }

    
    [HttpPost]
    public IActionResult Subscribe(SubscribeModel subscribe)
    {
        if (ModelState.IsValid)
        {
            //var result = _subscribeService.CreateNewSubscription(subscribe)
            // Add notification saying that subscription was succesful.
            return RedirectToAction(nameof(Index));
        }

        var viewModel = new HomeIndexViewModel();
        // Add an announcement function that opens a modal to confirm email subscription was succesful. Or similar
        return View(nameof(Index), viewModel);
    }
}