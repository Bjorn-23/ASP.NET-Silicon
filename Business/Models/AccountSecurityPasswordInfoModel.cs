using System.ComponentModel.DataAnnotations;


namespace Business.Models;

public class AccountSecurityPasswordInfoModel
{
    [Display(Name = "Current password", Prompt = "********", Order = 0)]
    [Required(ErrorMessage = "You must enter your current password")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;

    [Display(Name = "New password", Prompt = "********", Order = 1)]
    [Required(ErrorMessage = "You must enter a new password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$", ErrorMessage = "Password invalid")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm new password", Prompt = "********", Order = 2)]
    [Required(ErrorMessage = "You must confirm your new password")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match!")]
    public string ConfirmNewPassword { get; set; } = null!;
}
