using System.ComponentModel.DataAnnotations;

namespace Business.Utilities;

public class CheckboxRequired : ValidationAttribute
{
    public override bool IsValid(object? value) => value is bool b && b;
}
