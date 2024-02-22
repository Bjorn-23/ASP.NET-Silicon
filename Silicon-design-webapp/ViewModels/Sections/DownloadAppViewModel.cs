using Silicon_design_webapp.ViewModels.Components;

namespace Silicon_design_webapp.ViewModels.Sections;

public class DownloadAppViewModel
{
    public string Id { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<AppDisplayViewModel> AppDisplays { get; set; } = [];
}
