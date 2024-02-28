using Business.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AccountSecurityDeleteModel
{
    [CheckboxRequired(ErrorMessage = "You must check box to delete account")]
    [Display(Order = 0)]
    public bool Confirm { get; set; } = false;
}
