using System.ComponentModel.DataAnnotations;

namespace ScholarshipPortal.Models
{
    public class ContactViewModel
    {
        [Key]
        public int Id { get; set; }

        // FIX: Initialize non-nullable strings to clear CS8618
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required.")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        public string Message { get; set; } = string.Empty;

        public DateTime SubmissionDate { get; set; }
    }
}