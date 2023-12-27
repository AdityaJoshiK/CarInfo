using System.ComponentModel.DataAnnotations;

namespace CarInfo.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string EmailVerificationCode { get; set; }
        public bool IsEmailVerified { get; set; }
    }

   
}
