namespace IdentityServer.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            Succeeded = false;
            Token = string.Empty;
        }
        public string Token { get; set; }
        public bool Succeeded { get; set; }
    }
}
