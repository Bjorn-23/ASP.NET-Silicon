using Silicon_design_webapp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Silicon_design_webapp.Models;

public class SubscribeModel
{
    [Display(Name = "Daily Newsletter", Order = 0)]
    public bool DailyNewsletter { get; set; }

    [Display(Name = "Advertising Updates", Order = 1)]
    public bool AdvertisingUpdates { get; set; } = false;

    [Display(Name = "Week in Review", Order = 2)]
    public bool WeekInReview { get; set; } = false;

    [Display(Name = "Event Updates", Order = 3)]
    public bool EventUpdates { get; set; } = false;

    [Display(Name = "Startups Weekly", Order = 4)]
    public bool StartupsWeekly { get; set; } = false;

    [Display(Name = "Podcasts", Order = 5)]
    public bool Podcasts { get; set; } = false;

    [Display(Order = 6)]
    [Required(ErrorMessage = "Email required")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$", ErrorMessage = "Email invalid")]
    public string Email { get; set; } = null!;
}

