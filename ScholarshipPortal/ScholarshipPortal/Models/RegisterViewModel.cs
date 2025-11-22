using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Added for Key attribute
using System.Text.RegularExpressions;

namespace ScholarshipPortal.Models
{
    public class RegisterViewModel
    {
        // Personal Details
        [Required(ErrorMessage = "State of Domicile is required.")]
        public string StateOfDomicile { get; set; }

        [Required(ErrorMessage = "District is required.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Name (as in marks sheets) is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Mobile Number must be 10 digits.")]
        public string MobileNumber { get; set; }

        [Key] // *** IMPORTANT: EF Core Primary Key ***
        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [StringLength(100)]
        public string Email { get; set; }

        // Core Identity Details
        [Required(ErrorMessage = "Institute Code is required.")]
        [StringLength(20)]
        public string InstituteCode { get; set; }

        [Required(ErrorMessage = "Aadhaar Number is required.")]
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Aadhaar Number must be 12 digits.")]
        public string AadhaarNumber { get; set; }

        // Bank Details
        [Required(ErrorMessage = "Bank IFSC Code is required.")]
        [RegularExpression(@"^[A-Za-z]{4}0[A-Za-z0-9]{6}$", ErrorMessage = "Invalid IFSC Code format.")]
        public string BankIfscCode { get; set; }

        [Required(ErrorMessage = "Bank Account Number is required.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Account Number must be between 10 and 20 digits.")]
        public string BankAccountNumber { get; set; }

        [Required(ErrorMessage = "Bank Name is required.")]
        public string BankName { get; set; }

        // Security
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped] // Tell EF not to map this to the database, as it's only used for validation
        public string ConfirmPassword { get; set; }

        // Declaration
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree that the information furnished is true.")]
        public bool DeclarationAccepted { get; set; }
    }
}