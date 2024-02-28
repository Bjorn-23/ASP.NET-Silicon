using Business.Models;

namespace Silicon_design_webapp.ViewModels.Home;

public class ShowcaseViewModel
{
    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? SubHeading { get; set; }

    public LinkModel Link { get; set; } = new LinkModel();

    public string? BrandsText { get; set; } = null!;

    public List<ImageModel>? Brands { get; set; }

    public ImageModel ShowcaseImage { get; set; } = null!;

}
