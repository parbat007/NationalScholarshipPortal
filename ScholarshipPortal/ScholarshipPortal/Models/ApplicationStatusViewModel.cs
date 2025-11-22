using System;
using System.ComponentModel.DataAnnotations;

namespace ScholarshipPortal.Models
{
    public class ApplicationStatusViewModel
    {
        public int ApplicationId { get; set; }
        // FIX: Initialize strings to prevent CS8618
        public string SchemeName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
        public string StatusColor { get; set; } = string.Empty;
        public string InstituteVerificationDate { get; set; } = string.Empty;
    }

    public class DashboardSummaryViewModel
    {
        public int TotalApplied { get; set; }
        public int Approved { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
    }
}