//using Silicon_design_webapp.Models.Components;
using Silicon_design_webapp.Models.Components;

namespace Silicon_design_webapp.Models.Sections;

public class SubscribeViewModel
{
    public string Title { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public string Subheading { get; set; } = null!;
    public List<CheckBoxViewModel> Checkboxes { get; set; } = [];
    public EmailModel Email { get; set; } = new();
}
