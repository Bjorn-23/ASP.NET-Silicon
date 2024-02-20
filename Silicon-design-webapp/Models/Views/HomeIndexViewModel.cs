using Silicon_design_webapp.Models.Components;
using Silicon_design_webapp.Models.Sections;

namespace Silicon_design_webapp.Models.Views;

public class HomeIndexViewModel
{
    public string Title { get; set; } = "Task Management Assistant You're Gonna Love";

    public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
    {
        Id = "overview",
        Title = "Task Management Assistant You're Gonna Love",
        SubHeading = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
        Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Get started for free" },
        BrandsText = "The largest companies use our tools to work efficiently",
        Brands =
                [
                    new() { ImageUrl = "./img/showcase-logo1.svg", AltText = "Logo 1" },
                    new() { ImageUrl = "./img/showcase-logo2.svg", AltText = "Logo 2" },
                    new() { ImageUrl = "./img/showcase-logo3.svg", AltText = "Logo 3" },
                    new() { ImageUrl = "./img/showcase-logo4.svg", AltText = "Logo 4" }

                ],
        ShowcaseImage = new() { ImageUrl = "./img/showcase-dashboard.png", AltText = "Taskmanager dashboard" }
    };

    public FeaturesViewModel Features { get; set; } = new FeaturesViewModel
    {
        Id = "features",
        Title = "What Do You Get With Our Tool?",
        Text = "Make sure all your tasks are organized so you can set priorities and focus on what's important.",

        featuresBoxes =
        [
            new()
            {
                Link = new LinkViewModel() { ControllerName="Features", ActionName="Comments" },
                Image = new ImageViewModel() { ImageUrl = "./img/chat.svg", AltText = "Chat icon" },
                SubHeading = "Comments On Tasks",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Analytics" },
                Image = new ImageViewModel() { ImageUrl = "./img/presentation.svg", AltText = "graph trending up on screen" },
                SubHeading = "Task analytics",
                Text = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Teams" },
                Image = new ImageViewModel() { ImageUrl = "./img/add-group.svg", AltText = "Group icon" },
                SubHeading = "Multiple Assigneess",
                Text = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Notifications" },
                Image = new ImageViewModel() { ImageUrl = "./img/bell.svg", AltText = "Bell ringing" },
                SubHeading = "Notifications",
                Text = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Subtasks" },
                Image = new ImageViewModel() { ImageUrl = "./img/test.svg", AltText = "Checklist" },
                SubHeading = "Selections & Subtasks",
                Text = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Security" },
                Image = new ImageViewModel() { ImageUrl = "./img/shield.svg", AltText = "Shield icon" },
                SubHeading = "DataSecurity",
                Text = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras."
            }
        ]
    };
}
