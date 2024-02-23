using Silicon_design_webapp.Models;
using Silicon_design_webapp.ViewModels.Components;

namespace Silicon_design_webapp.ViewModels.Sections.Account;

public class AccountSidebarViewModel
{
    //This should be removed and the picture should instead be its own model that can be got from a database.
    public ImageViewModel ProfileImage { get; set; } = null!;

    public AccountDetailsBasicInfoModel AccountInfo { get; set; } = new();
}
