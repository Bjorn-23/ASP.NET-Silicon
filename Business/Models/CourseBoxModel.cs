using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class CourseBoxModel
{
    [Required]
    public string Id { get; set; } = null!;

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Author { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public string AltText { get; set; } = null!;

    public bool BestSeller { get; set; } = false;

    [Required]
    public string Currency { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    [Required]
    public string LengthInHours { get; set; } = null!;

    public string? Rating { get; set; }

    public int? CategoryId { get; set; }

    public string? Category { get; set; }
}

///REMAKE MODEL WITH THE BELOW PROPS if time.

////public bool Bookmark { get; set; } = false; -- move this to a table in User and add bookmarks table containing UserId, CourseId and BookmarkId
//public bool isDigital { get; set, } = false;
//public int RatingInPercentage { get; set; }
//public int NumberOfLikes { get; set; }
//public int NumberOfRatings { get; set; }
//public ProgramDetailsModel? Details { get; set; }
//(string Title, string description)
//    public string[] WhatYouLearn { get; set; }
//(string bulletPoint)
//    public ProgramIncludesModel Includes { get; set; }
//(icon, string text)
//    public string CourseDescription(string text)
//    public string Ingress { get; set; }
//(string text)