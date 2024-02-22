using Silicon_design_webapp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.Models;

public class SignUpModel
{
    [Required(ErrorMessage = "Invalid first name")]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    public string FirstName { get; set; } = null!;

    
    [Required(ErrorMessage = "Invalid last name")]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    public string LastName { get; set; } = null!;


    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter your Email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "Email invalid")]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Invalid password")]
    [Display(Name = "Password", Prompt = "********", Order = 3)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$", ErrorMessage = "Password invalid")]
    public string Password { get; set; } = null!;


    [Required(ErrorMessage = "You must confirm your password")]
    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage ="Passwords do not match!")]
    public string ConfirmPassword { get; set; } = null!;


    [CheckboxRequired(ErrorMessage = "You must accept the terms and conditions")]
    [Display(Order = 5)]
    public bool TermsConditions { get; set; } = false;
}
