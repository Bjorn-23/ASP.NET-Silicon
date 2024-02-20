using Silicon_design_webapp.Models.Components;

namespace Silicon_design_webapp.Models.Sections;

public class DarkModeSliderViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public ImageViewModel ImageOne { get; set; } = null!;
    public ImageViewModel ImageTwo { get; set;} = null!;

}