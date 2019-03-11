using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is Reuired")]
        [EmailAddress(ErrorMessage = "Email is not in correct format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}
