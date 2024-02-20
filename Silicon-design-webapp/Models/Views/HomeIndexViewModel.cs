﻿using Silicon_design_webapp.Models.Components;
using Silicon_design_webapp.Models.Sections;
using System.Reflection;

namespace Silicon_design_webapp.Models.Views;

public class HomeIndexViewModel
{
    public string Title { get; set; } = "Task Management Assistant You're Gonna Love";

    public ShowcaseViewModel Showcase { get; set; } = new()
    {
        Id = "overview",
        Title = "Task Management Assistant You're Gonna Love",
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

        featuresBoxes =
        [
            new()
            {
                Link = new LinkViewModel() { ControllerName="Features", ActionName="Comments" },
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
        Id = "DarkModeSlider",
        Title = "Switch between Light & Dark mode",
        ImageOne = new()
        {
            ImageUrl = "/img/mbp-dark-full.png",
            AltText= "before dark image"
        },
        ImageTwo = new()
        {
            ImageUrl = "/img/mbp-light-full.png",
            AltText= "after light image"
        }
    };

    public ManageYourWorkViewModel ManageYourWork { get; set; } = new()
    {
        Id = "ManageYourWork",
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
        Link = new LinkViewModel() { ControllerName="Features", ActionName="Index", Text="Learn more"}
    };

    public DownloadAppViewModel DownloadApp { get; set; } = new()
    {
        Id = "DownloadApp",
        Image = new()
        {
            ImageUrl = "./img/smartphone.png",
            AltText = "Smartphones"
        },
        Title = "Download Our App For Any Devices:",
        AppDisplay =
        [
            new AppDisplayViewModel()
            {
               Title = "App Store",
               Icon = "fa-solid fa-star",
               Description ="Editor's Choice",
               Rating = "rating 4.7, 187K+ reviews",
               Image = new()
               {
                   ImageUrl = "./img/appstore.svg",
                   AltText="Apples app store"
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


}
