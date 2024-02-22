using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels;

namespace Silicon_design_webapp.Controllers;

public class AuthController : Controller
{
    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public IActionResult SignUp(SignUpViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        //add logic for creating an account in service here.

        return RedirectToAction("SignIn", "Auth");
    }
}
