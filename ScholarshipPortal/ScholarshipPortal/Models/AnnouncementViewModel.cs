namespace ScholarshipPortal.Models
{
    public class AnnouncementViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // e.g., "New", "Urgent", "Info"
        public string Color { get; set; } // Used for Tailwind styling
    }
}