using Silicon_design_webapp.ViewModels.Components;

namespace Silicon_design_webapp.ViewModels;

public class CoursesViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = "Courses";
    public List<CourseBoxViewModel> Courses { get; set; } = new();
}
