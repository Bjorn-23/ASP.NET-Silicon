namespace Business.Models;

public class CourseBoxModel
{
    public string Id { get; set; } = null!;
    public LinkModel CourseLink { get; set; } = new();
    public ImageModel CourseImage { get; set; } = null!;
    public bool BestSeller { get; set; } = false;
    public bool Bookmark { get; set; } = false;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string Length { get; set; } = null!;
    public string Rating { get; set; } = null!;
}
