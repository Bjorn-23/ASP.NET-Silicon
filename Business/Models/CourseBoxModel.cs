namespace Business.Models;

public class CourseBoxModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string AltText { get; set; } = null!;

    //public bool Bookmark { get; set; } = false;
    public bool BestSeller { get; set; } = false;
    public string Currency { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string LengthInHours { get; set; } = null!;
    public string? Rating { get; set; }
}
