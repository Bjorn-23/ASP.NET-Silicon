using Business.Models;

namespace Silicon_design_webapp.ViewModels.Shared;

public class AppDisplayViewModel
{
    public string Title { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Rating { get; set; } = null!;
    public LinkModel Link { get; set; } = new LinkModel();
    public ImageModel Image { get; set; } = new ImageModel();
}
