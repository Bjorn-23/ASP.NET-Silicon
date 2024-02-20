using Silicon_design_webapp.Models.Components;

namespace Silicon_design_webapp.Models.Sections;

public class DownloadAppViewModel
{
    public string Id { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<AppDisplayViewModel> AppDisplay { get; set; } = [];
}
