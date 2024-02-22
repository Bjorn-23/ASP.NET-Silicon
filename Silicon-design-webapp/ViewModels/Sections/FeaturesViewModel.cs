using Silicon_design_webapp.ViewModels.Components;

namespace Silicon_design_webapp.ViewModels.Sections;

public class FeaturesViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public List<FeaturesBoxViewModel> FeaturesBoxes { get; set; } = [];
}
