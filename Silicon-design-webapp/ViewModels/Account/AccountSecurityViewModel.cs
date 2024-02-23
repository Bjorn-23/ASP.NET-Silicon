using Silicon_design_webapp.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountSecurityViewModel
{
    public string Title { get; set; } = "Security";

    public AccountSidebarViewModel Sidebar { get; set; } = new()
    {
        ProfileImage = new()
        {
            ImageUrl = "/img/user-logged-in.png",
            AltText = "User profice pic"
        },

        AccountInfo = new()
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = "bjorn@domain.com",
            Phone = "0789456123",
            Biography = null!,
        }
    };

    public AccountSecurityPasswordInfoModel Passwords { get; set; } = new();

    public AccountSecurityDeleteModel DeleteAccount { get; set; } = new();
}
