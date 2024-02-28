namespace Silicon_design_webapp.ViewModels.Shared;

public class CourseBoxViewModel
{
    public string Id { get; set; } = null!;
    public LinkViewModel CourseLink { get; set; } = new();
    public ImageViewModel CourseImage { get; set; } = null!;
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
