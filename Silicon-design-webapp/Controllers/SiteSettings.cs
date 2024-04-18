using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_design_webapp.Controllers
{
    public class SiteSettings : Controller
    {
        [HttpGet]
        public IActionResult ChangeTheme(string theme)
        {
            try
            {
                var option = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(60),
                };

                Response.Cookies.Append("ThemeMode", theme, option);
                return Ok();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return StatusCode(500);
        }

        [HttpPost]
        public IActionResult CookieConsent()
        {
            try
            {
                var option = new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1),
                    HttpOnly = true,
                    Secure = true,
                };

                Response.Cookies.Append("CookieConsent", "true", option);
                return Ok();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return StatusCode(500);
        }
    }
}
