using Silicon_design_webapp.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Silicon_design_webapp.ViewModels;

public class SignInViewModel
{
    public string Title = "Sign In";
    public SignInModel Form { get; set; } = new();
}
