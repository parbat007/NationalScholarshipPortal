using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ScholarshipPortal.Models
{
    // This comprehensive model holds all data across the three steps of the application.
    public class ScholarshipApplicationViewModel
    {
        // Primary Key for this application submission
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationId { get; set; }

        // Linked to the RegisterViewModel (User's Email, automatically populated on form start)
        [Required]
        public string UserId { get; set; } = string.Empty; // Store the logged-in user's email

        [Required]
        public string SchemeName { get; set; } = string.Empty; // From the Apply button link

        public DateTime SubmissionDate { get; set; }

        // === PAGE 1: BASIC & ACADEMIC DETAILS ===

        // Basic Details
        [Required(ErrorMessage = "Aadhaar Number is required.")]
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Aadhaar Number must be 12 digits.")]
        public string AadhaarNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Religion is required.")]
        public string Religion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Community/Category is required.")]
        public string CommunityCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Father's Name is required.")]
        public string FatherName { get; set; } = string.Empty;

        public string MotherName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Family Annual Income is required.")]
        [Range(0, 999999999.99, ErrorMessage = "Invalid income value.")]
        public decimal FamilyAnnualIncome { get; set; }

        // Academic Details (Present Course)
        [Required(ErrorMessage = "Institute Name is required.")]
        public string InstituteName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Present Class/Course is required.")]
        public string PresentClassCourse { get; set; } = string.Empty;

        [Required(ErrorMessage = "Present Class/Course Year is required.")]
        public string PresentClassCourseYear { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mode of Study is required.")]
        public string ModeOfStudy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Class Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime? ClassStartDate { get; set; }

        [Required(ErrorMessage = "University/Board Name is required.")]
        public string UniversityBoardName { get; set; } = string.Empty;

        // Academic Details (Previous Course)
        public string PreviousClassCourse { get; set; } = string.Empty;
        public int? PreviousPassingYear { get; set; }
        public decimal? PreviousClassPercentage { get; set; }

        // 10th Class Details
        public string Class10RollNumber { get; set; } = string.Empty;
        public string Class10BoardName { get ; set; } = string.Empty;
        public int? Class10PassingYear { get; set; }
        public decimal? Class10Percentage { get; set; }

        // 12th Class Details
        public string Class12RollNumber { get; set; } = string.Empty;
        public string Class12BoardName { get; set; } = string.Empty;
        public int? Class12PassingYear { get; set; }
        public decimal? Class12Percentage { get; set; }

        // === PAGE 2: CONTACT, FINANCIAL & PERSONAL DETAILS ===

        // Contact Details
        [Required(ErrorMessage = "State is required.")]
        public string ContactState { get; set; } = string.Empty;

        [Required(ErrorMessage = "District is required.")]
        public string ContactDistrict { get; set; } = string.Empty;

        [Required(ErrorMessage = "Block/Taluk is required.")]
        public string BlockTaluk { get; set; } = string.Empty;

        [Required(ErrorMessage = "House Number is required.")]
        public string HouseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street Number is required.")]
        public string StreetNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pincode is required.")]
        public string Pincode { get; set; } = string.Empty;

        // Fee Details
        [Required(ErrorMessage = "Admission Fee is required.")]
        public decimal AdmissionFee { get; set; }

        [Required(ErrorMessage = "Tuition Fee is required.")]
        public decimal TuitionFee { get; set; }

        [Required(ErrorMessage = "Other Fee is required.")]
        public decimal OtherFee { get; set; }

        // Other Personal Details
        [Required(ErrorMessage = "Disability Status is required.")]
        public bool IsDisabled { get; set; }

        public string TypeOfDisability { get; set; } = string.Empty;
        public decimal? DisabilityPercentage { get; set; }

        [Required(ErrorMessage = "Marital Status is required.")]
        public string MaritalStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Parents Profession is required.")]
        public string ParentsProfession { get; set; } = string.Empty;

        // === PAGE 3: DOCUMENTS & SUBMISSION ===

        // Documents (Using string placeholders for file paths/status)
        public string DomicileCertificatePath { get; set; } = string.Empty;
        public string StudentPhotographPath { get; set; } = string.Empty;
        public string InstituteIdCardPath { get; set; } = string.Empty;
        public string CasteIncomeCertificatePath { get; set; } = string.Empty;
        public string PreviousMarksheetPath { get; set; } = string.Empty;
        public string FeeReceiptPath { get; set; } = string.Empty;
        public string BankPassbookPath { get; set; } = string.Empty;
        public string AadhaarCardPath { get; set; } = string.Empty;
        public string Class10MarksheetPath { get; set; } = string.Empty;
        public string Class12MarksheetPath { get; set; } = string.Empty;


        // Final Declaration
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the declaration.")]
        public bool FinalDeclarationAccepted { get; set; }
    }
}