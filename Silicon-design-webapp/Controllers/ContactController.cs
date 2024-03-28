using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Contact;

namespace Silicon_design_webapp.Controllers;

public class ContactController : Controller
{
    [HttpGet("/contact")]
    public IActionResult Index()
    {
        var viewModel = new ContactViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Index(ContactModel model)
    {
        if (ModelState.IsValid)
        {
            // Add submit contactform info here.

            var viewModel = new ContactViewModel();
            viewModel.Form = new ContactModel();
            ModelState.Clear();
            viewModel.Form.StatusMessage = "Success - Form submitted";
            return View(nameof(Index), viewModel);
            //return RedirectToAction("Index");
        }
        else
        {
            var viewModel = new ContactViewModel();
            viewModel.Form.StatusMessage = "Error - Form could not be submitted";
            return View(nameof(Index), viewModel);
        }

    }
}
