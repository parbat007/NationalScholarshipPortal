using System.Collections.Generic;

namespace ScholarshipPortal.Models
{
    // This aggregates all necessary data for the dashboard view
    public class DashboardViewModel
    {
        public RegisterViewModel UserProfile { get; set; } // The user's registration data
        public DashboardSummaryViewModel Summary { get; set; }
        public List<ApplicationStatusViewModel> ApplicationStatuses { get; set; } = new List<ApplicationStatusViewModel>();
        public List<ProgramCardViewModel> AvailableSchemes { get; set; } = new List<ProgramCardViewModel>();
    }
}