using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Home;
using System.Text;

namespace Silicon_design_webapp.Controllers;

public class HomeController : Controller
{
    private readonly string _apiKey = "YmRhNGYwZDgtNDNkZi00N2EyLTliNmQtODYxZTA3OTQ3NDUy";

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();
        ViewData["Title"] = viewModel.Title;
        TempData["SubscriptionStatus"] = TempData["SubscriptionStatus"] ?? "";
        return View(viewModel);
    }

    
    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeModel subscribe)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(subscribe), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7034/api/Subscriptions?key={_apiKey}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SubscriptionStatus"] = "Subscription updated";
                return RedirectToAction(nameof(Index));
            }      
        }

        TempData["SubscriptionStatus"] = "Something went wrong";
        return RedirectToAction("Index");
    }
}