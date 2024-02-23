using Silicon_design_webapp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.Models;

public class AccountSecurityDeleteModel
{
    [CheckboxRequired(ErrorMessage = "You must check box to delete account")]
    [Display(Order = 0)]
    public bool Confirm { get; set; } = false;
}
