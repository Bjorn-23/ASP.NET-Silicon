using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";

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

    public AccountDetailsBasicInfoModel BasicForm { get; set; } = new();

    public AccountDetailsAddressInfoModel AddressForm { get; set; } = new();

}
