using Business.Models;

namespace Silicon_design_webapp.ViewModels.Courses;

public class CoursesViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = "Courses";
    public List<CourseBoxModel> Courses { get; set; } = new();
}
