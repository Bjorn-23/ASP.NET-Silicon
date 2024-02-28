﻿using Business.Models;

namespace Silicon_design_webapp.ViewModels.Account;

public class AccountSecurityViewModel
{
    public string Title { get; set; } = "Security";

    public string? ErrorMessage { get; set; } = "";

    public AccountSidebarViewModel Sidebar { get; set; } = new()
    {
        ProfileImage = new()
        {
            ImageUrl = "/img/user-logged-in.png",
            AltText = "User profice pic"
        },

        AccountInfo = new()
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = "bjorn@domain.com",
            Phone = "0789456123",
            Biography = "",
        }
    };

    public AccountSecurityPasswordInfoModel Form { get; set; } = new();

    public AccountSecurityDeleteModel DeleteAccount { get; set; } = new();
}
