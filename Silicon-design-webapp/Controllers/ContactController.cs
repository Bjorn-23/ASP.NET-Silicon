using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Contact;
using System.Text;

namespace Silicon_design_webapp.Controllers;

public class ContactController(IConfiguration configuration) : Controller
{

    private IConfiguration _configuration = configuration;
    #region CONTACT

    [HttpGet("/contact")]
    public IActionResult Index()
    {
        ViewData["StatusMessage"] = ViewData["StatusMessage"] ?? "";
        var viewModel = new ContactViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactModel model)
    {            
        var viewModel = new ContactViewModel();

        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await http.PostAsync($"https://localhost:7034/api/Contact?key={_configuration["ApiKey:Secret"]}", content);
            if (response.IsSuccessStatusCode)
            {
                ViewData["StatusMessage"] = "Success - Form submitted";
                viewModel.Form = new ContactModel();
                return View(viewModel);
            }
        }

        ViewData["StatusMessage"] = "Error - Form not submitted";
        return View(viewModel);
    }

    #endregion

}
