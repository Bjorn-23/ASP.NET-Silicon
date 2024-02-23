using Silicon_design_webapp.ViewModels.Components;

namespace Silicon_design_webapp.ViewModels.Home;

public class IntegrateToolsViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string SubHeading { get; set; } = null!;
    public List<ToolBoxViewModel> Boxes { get; set; } = null!;
}
