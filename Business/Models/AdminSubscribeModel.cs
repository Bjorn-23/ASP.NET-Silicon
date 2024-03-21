using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AdminSubscribeModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "Daily Newsletter", Order = 0)]
        public bool DailyNewsletter { get; set; }

        [Display(Name = "Advertising Updates", Order = 1)]
        public bool AdvertisingUpdates { get; set; }

        [Display(Name = "Week in Review", Order = 2)]
        public bool WeekInReview { get; set; }

        [Display(Name = "Event Updates", Order = 3)]
        public bool EventUpdates { get; set; }

        [Display(Name = "Startups Weekly", Order = 4)]
        public bool StartupsWeekly { get; set; }

        [Display(Name = "Podcasts", Order = 5)]
        public bool Podcasts { get; set; }

        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email", Prompt = "Enter your Email", Order = 2)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "Email invalid")]
        public string Email { get; set; } = null!;
    }
}
