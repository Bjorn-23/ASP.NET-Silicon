using Business.Models;

namespace Silicon_design_webapp.ViewModels.Admin;

public class AdminContactViewModel
{
    public IEnumerable<AdminContactModel> ContactForms { get; set; } = [];

    public AdminContactModel Contact { get; set; } = new();
}