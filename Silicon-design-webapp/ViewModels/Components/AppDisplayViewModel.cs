namespace Silicon_design_webapp.ViewModels.Components;

public class AppDisplayViewModel
{
    public string Title { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Rating { get; set; } = null!;
    public LinkViewModel Link { get; set; } = new LinkViewModel();
    public ImageViewModel Image { get; set; } = null!;
}
