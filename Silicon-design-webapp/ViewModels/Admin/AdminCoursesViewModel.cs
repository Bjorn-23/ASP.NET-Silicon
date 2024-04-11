using Business.Models;

namespace Silicon_design_webapp.ViewModels.Admin;

public class AdminCoursesViewModel
{
    public IEnumerable<CourseBoxModel> Courses { get; set; } = [];
    public CourseBoxModel Course { get; set; } = new();
    public CreateCourseModel CreateCourse { get; set; } = new();
    public IEnumerable<CategoryModel> Categories { get; set; } = [];

}
