using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountSecurityViewModel
{
    public string Title { get; set; } = "Security";

    public string? StatusMessage { get; set; } = "";

    public AccountSidebarViewModel Sidebar { get; set; } = new();

    public PasswordUpdateModel Form { get; set; } = new();

    public DeleteAccountModel DeleteAccount { get; set; } = new();
}
