using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.Models;

public class AccountSecurityPasswordInfoModel
{
    [Display(Name = "Current password", Prompt = "********", Order = 0)]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a new password")]
    [Display(Name = "New password", Prompt = "********", Order = 1)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$", ErrorMessage = "Password invalid")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "You must confirm your new password")]
    [Display(Name = "Confirm new password", Prompt = "Confirm your new password", Order = 2)]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match!")]
    public string ConfirmNewPassword { get; set; } = null!;
}
