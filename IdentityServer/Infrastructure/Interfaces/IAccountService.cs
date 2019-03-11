using System.Threading.Tasks;
using IdentityServer.Models;

namespace IdentityServer.Infrastructure
{
    public interface IAccountService
    {
        Task<RegistrationResponse> Registration(RegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<bool> CheckEmailExistence(string email);
        Task<bool> ConfirmEmail(string userId, string token);
    }
}
