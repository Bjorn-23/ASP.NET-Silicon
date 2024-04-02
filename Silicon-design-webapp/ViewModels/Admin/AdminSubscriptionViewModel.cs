using Business.Models;

namespace Silicon_design_webapp.ViewModels.Admin;

public class AdminSubscriptionViewModel
{
    public IEnumerable<AdminSubscribeModel> Subscribers { get; set; } = [];

    public AdminSubscribeModel Subscriber { get; set; } = new();
}
