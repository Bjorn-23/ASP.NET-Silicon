﻿using Business.Models;

namespace Silicon_design_webapp.ViewModels.Courses;

public class CoursesIndexViewModel
{
    public string Title { get; set; } = "Courses";

    //public CoursesViewModel Courses = new()
    //{
    //    Courses =
    //    [
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = true,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = true,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = false,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            //DiscountPrice = 5.99m,
    //            DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = false,
    //            Bookmark = false,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = false,
    //            Bookmark = false,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = true,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            //DiscountPrice = 5.99m,
    //            DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "180",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = true,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Hans Mattin-Lassei",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "95",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = true,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //        new CourseBoxModel()
    //        {
    //            CourseLink = new()
    //            {
    //                ControllerName = "Courses",
    //                ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
    //            },
    //            CourseImage = new()
    //            {
    //                ImageUrl = "./img/course1.png",
    //                AltText = "hands typing on laptop"
    //            },
    //            BestSeller = true,
    //            Bookmark = true,
    //            Title = "Fullstack Web Developer Course From Scratch",
    //            Author = "Robert Fox",
    //            Currency = "$",
    //            Price = 12.50m,
    //            DiscountPrice = 5.99m,
    //            //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
    //            LengthInHours = "220",
    //            Rating = "94% (4.2K)"
    //        },
    //    ]
    //};
}
