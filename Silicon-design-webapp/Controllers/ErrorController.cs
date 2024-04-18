using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_design_webapp.Controllers;

public class ErrorController : Controller
{
    
    [HttpGet("/error/404")]
    public IActionResult Error()
    {
        try
        {
            return View();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
