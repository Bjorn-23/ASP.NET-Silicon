using Business.Services;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silicon_design_webapp.ViewModels.Auth;
using System.Diagnostics;


namespace Silicon_design_webapp.Controllers;

public class AuthController(UserService userService, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserService _userService = userService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    #region SIGNUP
    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        try
        {
            var viewModel = new SignUpViewModel();
            ViewData["StatusMessage"] = TempData["StatusMessage"] ?? "";
            return View(viewModel);
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }


    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        //var error = ErrorChecker.ModelStateErrorChecker(ModelState);
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(viewModel.Form);
                if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
                {
                    TempData["StatusMessage"] = result.Message;
                    return RedirectToAction("SignIn", "Auth");
                }

                ViewData["StatusMessage"] = result.Message ?? "";
                return View(viewModel);
            }

            ViewData["StatusMessage"] = "Invalid form, please check all fields and try again";
            return View(viewModel);
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }
    #endregion

    #region SIGNIN
    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn(string returnurl)
    {
        try
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Details", "Account");

            var viewModel = new SignInViewModel();

            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnurl ?? "~/account";
            }
       
            string statusMessage = TempData["StatusMessage"]?.ToString() ?? "";
            ViewBag.StatusMessage = statusMessage;
        
            return View(viewModel);
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }


    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignInUserAsync(viewModel.Form);
                if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) 
                        return Redirect(returnUrl);

                    return RedirectToAction("Details", "Account");
                }
            }

            ModelState.Clear();
            ViewBag.StatusMessage = "Incorrect Email or Password";
            return View(viewModel);
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }
    #endregion

    #region SIGNOUT
    [Authorize]
    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }
    #endregion

    #region EXTERNAL ACCOUNTS

    #region Facebook
    [HttpGet]
    public  IActionResult Facebook(string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var authprops = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback", new { returnUrl }), returnUrl);
                return new ChallengeResult("Facebook", authprops);
            }

            TempData["StatusMessage"] = "Failed to login with Facebook - please try again later";
            return RedirectToAction("SignIn", "Account");
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }

    public async Task<IActionResult> FacebookCallback(string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info != null)
                {
                    var result = await _userService.SignInOrRegisterExternalAccount(info);
                    if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK && HttpContext.User != null)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                
                        return RedirectToAction("Details", "Account");
                    }
                }
            }

            TempData["StatusMessage"] = "Failed to login with Facebook - please try again later";
            return RedirectToAction("SignIn", "Account");
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }
    #endregion

    #region Google
    [HttpGet]
    public IActionResult Google(string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var authprops = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback", new { returnUrl }), returnUrl);
                return new ChallengeResult("Google", authprops);
            }

            TempData["StatusMessage"] = "Failed to login with Google - please try again later";
            return RedirectToAction("SignIn", "Account");
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }

    public async Task<IActionResult> GoogleCallback(string? returnUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info != null)
                {
                    var result = await _userService.SignInOrRegisterExternalAccount(info);
                    if (result.StatusCode == Infrastructure.Utilities.StatusCode.OK && HttpContext.User != null)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                
                        return RedirectToAction("Details", "Account");
                    }
                }
            }

            TempData["StatusMessage"] = "Failed to login with Google - please try again later";
            return RedirectToAction("SignIn", "Account");
        }
        catch (Exception ex) { Debug.Write(ex.Message); }
        return null!;
    }
    #endregion

    #endregion
}
