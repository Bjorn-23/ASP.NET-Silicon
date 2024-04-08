﻿namespace Business.Models;

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
    public int? CategoryId { get; set; }
    public string? Category { get; set; }
}

///REMAKE MODEL WITH THE BELOW PROPS

//public string Id { get; set; } = null!;
//public string Title { get; set; } = null!;
//public string Author { get; set; } = null!;
//public string ImageUrl { get; set; } = null!;
//public string AltText { get; set; } = null!;

////public bool Bookmark { get; set; } = false; -- move this to a table in User and add bookmarks as a 
//public bool isBestSeller { get; set; } = false;
//public bool isDigital
//{
//    get; set, } = false;
//    public string Currency { get; set; } = null!;
//public decimal Price { get; set; }
//public decimal? DiscountPrice { get; set; }
//public string LengthInHours { get; set; } = null!;
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