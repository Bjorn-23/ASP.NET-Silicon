using Business.Models;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Contact;
using System.Diagnostics;
using System.Text;

namespace Silicon_design_webapp.Controllers;

public class ContactController(IConfiguration configuration) : Controller
{
    private IConfiguration _configuration = configuration;

    #region CONTACT

    [HttpGet("/contact")]
    public IActionResult Index()
    {
        try
        {
            ViewData["StatusMessage"] = TempData["StatusMessage"] ?? "";
            var viewModel = new ContactViewModel();
            return View(viewModel);
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return StatusCode(500);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactModel model)
    {

        if (ModelState.IsValid)
        {
            try
            {
                using var http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await http.PostAsync($"https://localhost:7034/api/Contact?key={_configuration["ApiKey:Secret"]}", content);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["StatusMessage"] = "Success - Form submitted";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["StatusMessage"] = $"Error - {response.StatusCode}";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                TempData["StatusMessage"] = ResponseFactory.InternalServerError().Message;
                return RedirectToAction("Index");
            }
        }

        var viewModel = new ContactViewModel();
        ViewData["StatusMessage"] = "Error - Form could not be submitted";
        return View(viewModel);
    }

    #endregion

}
