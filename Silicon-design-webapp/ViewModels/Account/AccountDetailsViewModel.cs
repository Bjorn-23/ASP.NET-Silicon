using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountDetailsViewModel
{
    public AccountSidebarViewModel Sidebar { get; set; } = new();
    
    public BasicInfoModel BasicForm { get; set; } = new();

    public AddressInfoModel AddressForm { get; set; } = new();

}
