using System.ComponentModel.DataAnnotations;

namespace ScholarshipPortal.Models
{
    public class InstituteLoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
