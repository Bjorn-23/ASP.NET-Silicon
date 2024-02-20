using Silicon_design_webapp.Models.Components;

namespace Silicon_design_webapp.Models.Sections;

public class FeaturesViewModel
{
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public List<FeaturesBoxViewModel> FeaturesBoxes { get; set; } = [];
}
