using Silicon_design_webapp.Models;
using Silicon_design_webapp.ViewModels.Shared;

namespace Silicon_design_webapp.ViewModels.Home;

public class HomeIndexViewModel
{
    public string Title { get; set; } = "Task Management Assistant You're Gonna Love";

    public ShowcaseViewModel Showcase { get; set; } = new()
    {
        Id = "overview",
        Title = "The Task Management Assistant You're Gonna Love",
        SubHeading = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
        Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Get started for free" },
        BrandsText = "The largest companies use our tools to work efficiently",
        Brands =
                [
                    new() { ImageUrl = "/img/showcase-logo1.svg", AltText = "Logo 1" },
                    new() { ImageUrl = "/img/showcase-logo2.svg", AltText = "Logo 2" },
                    new() { ImageUrl = "/img/showcase-logo3.svg", AltText = "Logo 3" },
                    new() { ImageUrl = "/img/showcase-logo4.svg", AltText = "Logo 4" }

                ],
        ShowcaseImage = new() { ImageUrl = "/img/showcase-dashboard.png", AltText = "Taskmanager dashboard" }
    };

    public FeaturesViewModel Features { get; set; } = new()
    {
        Id = "features",
        Title = "What Do You Get With Our Tool?",
        Text = "Make sure all your tasks are organized so you can set priorities and focus on what's important.",

        FeaturesBoxes =
        [
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Comments" },
                Image = new ImageViewModel() { ImageUrl = "/img/chat.svg", AltText = "Chat icon" },
                SubHeading = "Comments On Tasks",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Analytics" },
                Image = new ImageViewModel() { ImageUrl = "/img/presentation.svg", AltText = "graph trending up on screen" },
                SubHeading = "Task analytics",
                Text = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Teams" },
                Image = new ImageViewModel() { ImageUrl = "/img/add-group.svg", AltText = "Group icon" },
                SubHeading = "Multiple Assigneess",
                Text = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Notifications" },
                Image = new ImageViewModel() { ImageUrl = "/img/bell.svg", AltText = "Bell ringing" },
                SubHeading = "Notifications",
                Text = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Subtasks" },
                Image = new ImageViewModel() { ImageUrl = "/img/test.svg", AltText = "Checklist" },
                SubHeading = "Selections & Subtasks",
                Text = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus."
            },
            new()
            {
                Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Security" },
                Image = new ImageViewModel() { ImageUrl = "/img/shield.svg", AltText = "Shield icon" },
                SubHeading = "DataSecurity",
                Text = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras."
            }
        ]
    };

    public DarkModeSliderViewModel DarkModeSlider { get; set; } = new()
    {
        Id = "darkModeSlider",
        Title = "Switch between Light & Dark mode",
        ImageOne = new()
        {
            ImageUrl = "/img/mbp-dark-full.png",
            AltText = "before dark image"
        },
        ImageTwo = new()
        {
            ImageUrl = "/img/mbp-light-full.png",
            AltText = "after light image"
        }
    };

    public ManageYourWorkViewModel ManageYourWork { get; set; } = new()
    {
        Id = "manageYourWork",
        Title = "Manage Your Work",
        Image = new()
        {
            ImageUrl = "/img/dashboard-regular.png",
            AltText = "Dashboard"
        },
        Checklist =
        [
            new CheckListViewModel() { Text = "Powerful project management" },
            new CheckListViewModel() { Text = "Transparent work management" },
            new CheckListViewModel() { Text = "Manage work & focus on the most important tasks" },
            new CheckListViewModel() { Text = "Track your progress with interactive charts" },
            new CheckListViewModel() { Text = "Easiest way to track time spent on tasks" }
        ],
        Link = new LinkViewModel() { ControllerName = "Features", ActionName = "Index", Text = "Learn more" }
    };

    public DownloadAppViewModel DownloadApp { get; set; } = new()
    {
        Id = "downloadApp",
        Image = new()
        {
            ImageUrl = "./img/smartphone.png",
            AltText = "Smartphones"
        },
        Title = "Download Our App For Any Devices:",
        AppDisplays =
        [
            new AppDisplayViewModel()
            {
                Title = "App Store",
                Icon = "fa-solid fa-star",
                Description = "Editor's Choice",
                Rating = "rating 4.7, 187K+ reviews",
                Image = new()
                {
                    ImageUrl = "./img/appstore.svg",
                    AltText = "Apples app store"
                }
            },
            new AppDisplayViewModel()
            {
                Title = "Goole Play",
                Icon = "fa-solid fa-star",
                Description = "App of the Day",
                Rating = "rating 4.8, 30K+ reviews",
                Image = new()
                {
                    ImageUrl = "./img/googleplay.svg",
                    AltText = "Googles play store"
                }
            }
        ]
    };

    public IntegrateToolsViewModel IntegrateTools { get; set; } = new()
    {
        Id = "integrateTools",
        Title = "Integrate Top Work Tools",
        SubHeading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat mollis egestas. Nam luctus facilisis ultrices. Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus",
        Boxes =
        [
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/Google.svg",
                    AltText = "Google Logo"
                },
                Text = "Lorem magnis pretium sed curabitur nunc facilisi nunc cursus sagittis."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/Zoom.svg",
                    AltText = "Google Logo"
                },
                Text = "In eget a mauris quis. Tortor dui tempus quis integer est sit natoque placerat dolor."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/color.svg",
                    AltText = "Google Logo"
                },
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/gmail.svg",
                    AltText = "Google Logo"
                },
                Text = "Rutrum interdum tortor, sed at nulla. A cursus bibendum elit purus cras praesent."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/blue-circle.svg",
                    AltText = "Google Logo"
                },
                Text = "Congue pellentesque amet, viverra curabitur quam diam scelerisque fermentum urna."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/survey-monkey.svg",
                    AltText = "Google Logo"
                },
                Text = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/dropbox.svg",
                    AltText = "Google Logo"
                },
                Text = "Ut in turpis consequat odio diam lectus elementum. Est faucibus blandit platea."
            },
            new ToolBoxViewModel()
            {
                Image = new ImageViewModel()
                {
                    ImageUrl = "./img/evernote.svg",
                    AltText = "Google Logo"
                },
                Text = "Faucibus cursus maecenas lorem cursus nibh. Sociis sit risus id. Sit facilisis dolor arcu."
            },
        ]
    };

    public SubscribeViewModel Subscribe = new()
    {
        Title = "Don't Want To Miss Anything?",
        Image = new() { ImageUrl = "./img/blue-squiggle.svg", AltText = "blue squiggle" },
        Subheading = "Sign Up For Newsletters"
    };
}
