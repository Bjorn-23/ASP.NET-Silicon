using Business.Models;

namespace Silicon_design_webapp.ViewModels.Courses;

public class CoursesViewModel
{
    public string Title { get; set; } = "Courses";

    public IEnumerable<CourseBoxModel> Courses { get; set; } = [];

    public IEnumerable<CategoryModel> Categories { get; set; } = [];

    public IEnumerable<SavedCoursesModel> SavedCourses { get; set; } = [];

    public Pagination Pagination { get; set; } = new();

    public string? CategoryQuery { get; set; }
}
