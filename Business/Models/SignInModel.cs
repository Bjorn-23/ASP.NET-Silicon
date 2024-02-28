using System.ComponentModel.DataAnnotations;


namespace Business.Models;

public class SignInModel
{
    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter your Email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "Email Or Password Invalid")]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Must enter password")]
    [Display(Name = "Password", Prompt = "********", Order = 3)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$", ErrorMessage = "Email Or Password Invalid")]
    public string Password { get; set; } = null!;


    [Display(Name = "Remember Me", Order = 5)]
    public bool RememberMe { get; set; } = false;
}
