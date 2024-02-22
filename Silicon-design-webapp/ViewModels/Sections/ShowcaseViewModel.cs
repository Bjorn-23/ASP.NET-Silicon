﻿using Silicon_design_webapp.ViewModels.Components;

namespace Silicon_design_webapp.ViewModels.Sections;


public class ShowcaseViewModel
{
    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? SubHeading { get; set; }

    public LinkViewModel Link { get; set; } = new LinkViewModel();

    public string? BrandsText { get; set; } = null!;

    public List<ImageViewModel>? Brands { get; set; }

    public ImageViewModel ShowcaseImage { get; set; } = null!;

}