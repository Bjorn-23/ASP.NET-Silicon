using Silicon_design_webapp.Models;
using Silicon_design_webapp.ViewModels.Components;
using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.ViewModels.Sections.Account;

public class AccountSidebarViewModel
{
    public ImageViewModel ProfileImage { get; set; } = null!;

    public AccountDetailsBasicInfoModel AccountInfo { get; set; } = new();
}
