namespace Silicon_design_webapp.ViewModels.Components;

public class FeaturesBoxViewModel
{
    public LinkViewModel Link { get; set; } = new LinkViewModel();

    public ImageViewModel Image { get; set; } = new ImageViewModel();

    public string SubHeading { get; set; } = null!;

    public string Text { get; set; } = null!;
}
