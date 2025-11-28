using System.ComponentModel.DataAnnotations;

namespace ScholarshipPortal.Models

{

    public class InstituteRegistration

    {

        [Key]

        public int InstituteId { get; set; }

        // ================= INSTITUTE DETAILS =================

        [Required(ErrorMessage = "Institute Name is required")]

        public string InstituteName { get; set; }

        [Required(ErrorMessage = "Institute Code is required")]

        public string InstituteCode { get; set; }

        // 🟢 NEW FIELD REQUIRED FOR LOGIN (this was missing)

        [Required(ErrorMessage = "Email is required")]

        [EmailAddress(ErrorMessage = "Enter a valid email")]

        public string Email { get; set; }

        public string? DISECode { get; set; }

        [Required(ErrorMessage = "State is required")]

        public string State { get; set; }

        [Required(ErrorMessage = "District is required")]

        public string District { get; set; }

        public string? Location { get; set; }

        public string? InstituteType { get; set; }

        public string? YearAdmissionStarted { get; set; }

        [Required(ErrorMessage = "Address is required")]

        public string Address { get; set; }

        // ================= AFFILIATION DETAILS =================

        public string? AffiliatedUniversityState { get; set; }

        public string? UniversityBoardName { get; set; }

        public string? EstablishmentCertificate { get; set; }

        public string? AffiliationCertificate { get; set; }

        // ================= PRINCIPAL & CONTACT =================

        [Required(ErrorMessage = "Principal Name is required")]

        public string PrincipalName { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]

        public string MobileNumber { get; set; }

        public string? Telephone { get; set; }

        // ================= LOGIN & SECURITY =================

        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]

        [Compare("Password", ErrorMessage = "Passwords do not match")]

        public string ConfirmPassword { get; set; }

        public string? SecurityQuestion { get; set; }

        public string? SecurityAnswer { get; set; }

        // =======================

        // STATE NODAL OFFICER STATUS

        // =======================

        // "Pending", "ForwardedToMinistry", "Rejected"

        public string StateStatus { get; set; } = "Pending";

        public string? StateRemarks { get; set; }

        public string MinistryStatus { get; set; } = "Pending";

        public string? MinistryRemarks { get; set; }

        public DateTime? MinistryActionDate { get; set; }

        public DateTime? StateActionDate { get; set; }

    }

}

