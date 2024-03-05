using Business.Models;

namespace Silicon_design_webapp.ViewModels.Auth;

public class SignUpViewModel
{
    public string? ErrorMessage { get; set; }
    public SignUpModel Form { get; set; } = new();
}
