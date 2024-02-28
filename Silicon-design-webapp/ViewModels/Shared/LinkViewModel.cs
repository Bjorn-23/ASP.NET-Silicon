namespace Silicon_design_webapp.ViewModels.Shared;

public class LinkViewModel
{
    public string ControllerName { get; set; } = null!;
    public string ActionName { get; set; } = null!;
    public string? FragmentName { get; set; }
    public string? Text { get; set; }

}
