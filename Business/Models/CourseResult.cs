namespace Business.Models;

public class CourseResult
{
    public Pagination Pagination { get; set; } = new();

    public IEnumerable<CourseBoxModel> ReturnCourses { get; set; } = [];
}
