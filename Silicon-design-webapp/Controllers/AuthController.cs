using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Auth;
using System.Security.Claims;

namespace Silicon_design_webapp.Controllers;

public class AuthController(UserService userService) : Controller
{
    private readonly UserService _userService = userService;



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
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.CreateUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
                return RedirectToAction("SignIn", "Auth");
        }
        
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }


    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.SignInUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK && result.ContentResult != null)
            {
                var user = (UserEntity)result.ContentResult;
                var claims = new List<Claim>()
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                await HttpContext.SignInAsync("AuthCookie", new ClaimsPrincipal(new ClaimsIdentity(claims, "AuthCookie")));
                return RedirectToAction("Details", "Account");
            }
        }

        ModelState.Clear();
        viewModel.ErrorMessage = "Incorrect Email or Password";
        return View(viewModel);
    }

    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }
}
