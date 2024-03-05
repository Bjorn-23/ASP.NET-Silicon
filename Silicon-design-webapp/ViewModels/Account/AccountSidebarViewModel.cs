using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountSidebarViewModel
{
    //This should be removed and the picture should instead be its own model that can be got from a database.
    public ImageModel ProfileImage { get; set; } = new()
    {
        ImageUrl = "/img/user-logged-in.png",
        AltText = "" // not used in account sidebar, using razor code instead.
    };
    public BasicInfoModel AccountInfo { get; set; } = new();
}
