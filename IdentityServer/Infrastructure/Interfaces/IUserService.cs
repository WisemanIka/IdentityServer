using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.Models;

namespace IdentityServer.Infrastructure
{
    public interface IUserService
    {
        List<User> GetUserData(List<string> userIds);
    }
}
