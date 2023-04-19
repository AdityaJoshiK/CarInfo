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
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public List<Error> errors { get; set; }
    }
}
