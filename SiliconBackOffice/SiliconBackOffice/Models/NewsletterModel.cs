namespace SiliconBackOffice.Models
{
    public class NewsletterModel
    {
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public bool AdvertisingUpdates { get; set; }
        public bool WeekInReview { get; set; }
        public bool EventUpdates { get; set; }
        public bool StartupsWeekly { get; set; }
        public bool Podcasts { get; set; }

    }
}
