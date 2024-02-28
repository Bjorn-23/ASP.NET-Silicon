using Business.Models;

namespace Silicon_design_webapp.ViewModels.Contact;

public class ContactViewModel
{
    public string Title { get; set; } = "Contact";

    public ContactModel Form { get; set; } = new();

}
