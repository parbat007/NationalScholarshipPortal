using System.ComponentModel.DataAnnotations;

namespace ScholarshipPortal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mobile Number or Email is required.")]
        public string Username { get; set; } // Can be mobile or email

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}