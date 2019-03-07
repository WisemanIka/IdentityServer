using System.Collections.Generic;

namespace IdentityServer.Models
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Email { get; set; }
    }
}
