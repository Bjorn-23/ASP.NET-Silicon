using Business.Models;

namespace Silicon_design_webapp.ViewModels.Shared;

public class FeaturesBoxViewModel
{
    public LinkModel Link { get; set; } = new LinkModel();

    public ImageModel Image { get; set; } = new ImageModel();

    public string SubHeading { get; set; } = null!;

    public string Text { get; set; } = null!;
}
