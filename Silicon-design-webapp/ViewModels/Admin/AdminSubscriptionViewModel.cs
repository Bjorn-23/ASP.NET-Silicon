using Business.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Silicon_design_webapp.ViewModels.Admin;

public class AdminSubscriptionViewModel
{
    public IEnumerable<AdminSubscribeModel> Subscribers { get; set; } = [];
}
