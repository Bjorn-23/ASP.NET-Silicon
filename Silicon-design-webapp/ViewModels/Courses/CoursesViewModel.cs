using Business.Models;

namespace Silicon_design_webapp.ViewModels.Courses;

public class CoursesViewModel
{
    //public string Id { get; set; } = null!;
    public string Title { get; set; } = "Courses";
    public IEnumerable<CourseBoxModel> Courses { get; set; } = [];
    
    //public IEnumerable<UserBookMarks> Bookmarks { get; set; } = []; // Add user bookmarks and loop through them so that they line up with the right Id.
}
