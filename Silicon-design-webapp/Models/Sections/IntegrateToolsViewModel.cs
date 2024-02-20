using Silicon_design_webapp.Models.Components;

namespace Silicon_design_webapp.Models.Sections;

public class IntegrateToolsViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string SubHeading { get; set; } = null!;
    public List<ToolBoxViewModel> Boxes { get; set; } = null!;
}
