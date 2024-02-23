using Silicon_design_webapp.Models;


namespace Silicon_design_webapp.ViewModels.Auth;

public class SignInViewModel
{
    public string Title = "Sign In";
    public SignInModel Form { get; set; } = new();
}
