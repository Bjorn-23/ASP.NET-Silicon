using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.Models;

public class SignInModel
{
    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter your Email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Invalid password")]
    [Display(Name = "Password", Prompt = "********", Order = 3)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember Me", Order = 5)]
    public bool RememberMe { get; set; } = false;
}
