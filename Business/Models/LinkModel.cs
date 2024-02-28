namespace Business.Models;

public class LinkModel
{
    public string ControllerName { get; set; } = null!;
    public string ActionName { get; set; } = null!;
    public string? FragmentName { get; set; }
    public string? Text { get; set; }
}
