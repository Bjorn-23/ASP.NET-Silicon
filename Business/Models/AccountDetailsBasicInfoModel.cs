using System.ComponentModel.DataAnnotations;


namespace Business.Models;

public class AccountDetailsBasicInfoModel
{
    public string? Id { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    [MinLength(2, ErrorMessage = "Name too short")]
    public string FirstName { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    [MinLength(2, ErrorMessage = "Name too short")]
    public string LastName { get; set; } = null!;


    [Required(ErrorMessage = "Must enter email")]
    [Display(Name = "Email", Prompt = "Enter your Email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "Email invalid")]
    public string Email { get; set; } = null!;


    [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }


    [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
    [DataType(DataType.MultilineText)]
    public string? Biography { get; set; }
}
