using System.Threading.Tasks;
using IdentityServer.Models;

namespace IdentityServer.Infrastructure
{
    public interface IAccountService
    {
        Task<RegistrationResponse> Registration(RegistrationRequest request);
    }
}
