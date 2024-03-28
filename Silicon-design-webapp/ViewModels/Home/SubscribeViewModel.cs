using Business.Models;

namespace Silicon_design_webapp.ViewModels.Home;

public class SubscribeViewModel
{
    public string Id = "subscribe";
    public string Title { get; set; } = null!;
    public ImageModel Image { get; set; } = new ImageModel();
    public string Subheading { get; set; } = null!;
    public SubscribeModel Subscribe { get; set; } = new();
}