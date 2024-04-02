namespace Business.Models;

public class AdminContactModel
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Services { get; set; }

    public string Message { get; set; } = null!;
}
