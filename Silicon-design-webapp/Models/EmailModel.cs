using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.Models;
public class EmailModel
{
    //[Display(Name = "", Prompt = "Your Email", Order = 0)]
    [Required(ErrorMessage = "Email required")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression("/ ^\\w + ([\\.-] ? w +) *@\\w + ([\\.-] ?\\w +)*(\\.\\w{2,})+$/", ErrorMessage = "Email invalid")]
    public string Email { get; set; } = null!;
}
