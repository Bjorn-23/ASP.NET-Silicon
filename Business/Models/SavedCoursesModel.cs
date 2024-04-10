using Infrastructure.Entitites;

namespace Business.Models;

public class SavedCoursesModel
{
    public string CourseId { get; set; } = null!;

    public string UserId { get; set; } = null!;
}