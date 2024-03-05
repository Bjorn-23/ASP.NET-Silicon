using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountSavedCoursesViewModel
{
    public AccountSidebarViewModel Sidebar { get; set; } = new();

    public List<CourseBoxModel> Courses { get; set; } =
        [
            new CourseBoxModel()
            {
                CourseLink = new()
                {
                    ControllerName = "Courses",
                    ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
                },
                CourseImage = new()
                {
                    ImageUrl = "/img/course1.png",
                    AltText = "hands typing on laptop"
                },
                BestSeller = true,
                Bookmark = true,
                Title = "Fullstack Web Developer Course From Scratch",
                Author = "Robert Fox",
                Currency = "$",
                Price = 12.50m,
                DiscountPrice = 5.99m,
                //DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
                Length = "220",
                Rating = "94% (4.2K)"
            },
            new CourseBoxModel()
            {
                CourseLink = new()
                {
                    ControllerName = "Courses",
                    ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
                },
                CourseImage = new()
                {
                    ImageUrl = "/img/course1.png",
                    AltText = "hands typing on laptop"
                },
                BestSeller = true,
                Bookmark = true,
                Title = "Fullstack Web Developer Course From Scratch",
                Author = "Alberto Flores",
                Currency = "$",
                Price = 25.99m,
                //DiscountPrice = 5.99m,
                DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
                Length = "350",
                Rating = "96% (4.2K)"
            },
            new CourseBoxModel()
            {
                CourseLink = new()
                {
                    ControllerName = "Courses",
                    ActionName = "details" //need to add an asp-aspect prop to LinkViewModel 
                },
                CourseImage = new()
                {
                    ImageUrl = "/img/course1.png",
                    AltText = "hands typing on laptop"
                },
                BestSeller = false,
                Bookmark = true,
                Title = "Fullstack Web Developer Course From Scratch",
                Author = "Robert Fox",
                Currency = "$",
                Price = 12.50m,
                //DiscountPrice = 5.99m,
                DiscountPrice = 0m, //When price is less than 1 the discount price will be hidden, and price will not be strikethrough.
                Length = "160",
                Rating = "92% (4.2K)"
            }
        ];
}
