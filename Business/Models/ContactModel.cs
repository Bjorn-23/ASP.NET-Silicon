using System.ComponentModel.DataAnnotations;


namespace Business.Models;

public class ContactModel
{
    //public string? StatusMessage { get; set; } = "";

    [Display(Name ="Full name", Prompt="Enter your full name", Order = 0)]
    [DataType(DataType.Text)]
    [Required(ErrorMessage="Full name is required")]
    [MinLength(2, ErrorMessage = "Name too short")]
    public string FullName { get; set; } = null!;


    [Display(Name="Email address", Prompt ="Enter your email address", Order= 1)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage ="Email address is required")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "Email invalid")]
    public string Email { get; set; } = null!;


    [Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
    [DataType(DataType.Text)]
    public string? Services { get; set; } = "";


    [Display(Name ="Message", Prompt ="Enter your message here")]
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage ="Message can not be empty")]
    public string  Message { get; set; } = null!;

}
