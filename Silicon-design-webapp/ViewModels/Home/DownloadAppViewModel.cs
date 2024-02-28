using Business.Models;
using Silicon_design_webapp.ViewModels.Shared;

namespace Silicon_design_webapp.ViewModels.Home;

public class DownloadAppViewModel
{
    public string Id { get; set; } = null!;
    public ImageModel Image { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<AppDisplayViewModel> AppDisplays { get; set; } = [];
}
