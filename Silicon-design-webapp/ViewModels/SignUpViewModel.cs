using Silicon_design_webapp.Models;

namespace Silicon_design_webapp.ViewModels;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign Up";
    public SignUpModel Form { get; set; } = new();
}
