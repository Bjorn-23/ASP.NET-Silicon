using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountSavedCoursesViewModel
{
    public AccountSidebarViewModel Sidebar { get; set; } = new();

    public List<CourseBoxModel> Courses { get; set; } = [];
}
