using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Firstname is Required")]
        [MinLength(2)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is Required")]
        [MinLength(4)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email is not in correct format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The Agreement is Required")]
        public bool IsAgreed { get; set; }
    }
}
