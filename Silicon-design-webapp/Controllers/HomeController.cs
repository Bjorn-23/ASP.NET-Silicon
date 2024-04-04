using Azure;
using Business.Models;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Home;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Silicon_design_webapp.Controllers;

public class HomeController(IConfiguration configuration) : Controller
{
    private readonly IConfiguration _configuration = configuration;
   
    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();
        try
        {
            ViewData["Title"] = viewModel.Title;
            TempData["SubscriptionStatus"] = TempData["SubscriptionStatus"] ?? "";
            return View(viewModel);

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeModel subscribe)
    {
        if (ModelState.IsValid)
        {
            try
            {
                using var http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(subscribe), Encoding.UTF8, "application/json");
                var response = await http.PostAsync($"https://localhost:7034/api/Subscriptions?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {                
                    TempData["SubscriptionStatus"] =   response.ReasonPhrase == "Created" ? "Subscription created" : "Subscription updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["SubscriptionStatus"] = $"Error - {response.ReasonPhrase}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var error = ResponseFactory.InternalServerError("please contact site owner if problem persists");
                TempData["SubscriptionStatus"] = $"{error.StatusCode} 500, {error.Message}";
                return RedirectToAction("Index");
            }
        }

        TempData["SubscriptionStatus"] = "Email must be valid";
        return RedirectToAction("Index");
    }
}