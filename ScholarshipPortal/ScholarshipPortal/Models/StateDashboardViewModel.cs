using System.Collections.Generic;

namespace ScholarshipPortal.Models
{
    public class StateDashboardViewModel
    {
        public string OfficerName { get; set; } = string.Empty;

        // Student scholarship applications
        public string? StudentSearch { get; set; }
        public int StudentPage { get; set; }
        public int StudentTotalPages { get; set; }
        public List<ScholarshipApplicationViewModel> StudentApplications { get; set; }
            = new List<ScholarshipApplicationViewModel>();

        // Institute registrations
        public string? InstituteSearch { get; set; }
        public int InstitutePage { get; set; }
        public int InstituteTotalPages { get; set; }
        public List<InstituteRegistration> Institutes { get; set; }
            = new List<InstituteRegistration>();
    }
}
