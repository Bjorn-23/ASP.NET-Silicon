using Business.Models;

namespace Silicon_design_webapp.ViewModels.Courses;

public class CourseDetailsViewModel()
{
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public CourseBoxModel Course { get; set; } = new();

}
