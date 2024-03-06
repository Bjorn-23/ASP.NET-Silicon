using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Auth;


namespace Silicon_design_webapp.Controllers;

public class AuthController(UserService userService, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserService _userService = userService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    #region SignUp
    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }


    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.RegisterUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
                return RedirectToAction("SignIn", "Auth");
        }

        //add errormessage
        return View(viewModel);
    }
    #endregion

    #region SignIn
    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn(string returnurl)
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        var viewModel = new SignInViewModel();
        ViewData["ReturnUrl"] = returnurl ?? Url.Content("~/");
        return View(viewModel);
    }


    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnurl)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.SignInUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
            {
                if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl) && returnurl != "/") 
                    return Redirect(returnurl);

                return RedirectToAction("Details", "Account");
            }
        }

        ModelState.Clear();
        viewModel.ErrorMessage = "Incorrect Email or Password";
        return View(viewModel);
    }
    #endregion

    #region SignOut
    [Authorize]
    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }
    #endregion
}
