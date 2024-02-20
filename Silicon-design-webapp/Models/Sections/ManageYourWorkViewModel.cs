﻿using Silicon_design_webapp.Models.Components;

namespace Silicon_design_webapp.Models.Sections;

public class ManageYourWorkViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public List<CheckListViewModel> Checklist { get; set; } = null!;
    public LinkViewModel Link { get; set; } = new LinkViewModel();

}
