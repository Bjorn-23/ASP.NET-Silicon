using Business.Models;

namespace Silicon_design_webapp.ViewModels.Admin;

public class AdminViewModel
{
    public AdminSearchModel SearchModel { get; set; } = new();

    public BasicInfoModel? BasicInfo { get; set; }

    public IEnumerable<BasicInfoModel>? Users { get; set; }

    public AddressInfoModel? AddressInfo { get; set; }
    
    public IEnumerable<AddressInfoModel>? Addresses { get; set; }

    public PasswordUpdateModel? PasswordUpdate { get; set; }

    public DeleteAccountModel? DeleteAccount { get; set; }

}
