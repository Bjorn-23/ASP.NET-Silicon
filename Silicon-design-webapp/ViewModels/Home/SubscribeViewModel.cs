using Silicon_design_webapp.Helpers;
using Silicon_design_webapp.Models;
using Silicon_design_webapp.ViewModels.Shared;

namespace Silicon_design_webapp.ViewModels.Home;

public class SubscribeViewModel
{
    public string Id = "subscribe";
    public string Title { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public string Subheading { get; set; } = null!;
    public SubscribeModel Subscribe { get; set; } = new();   
}