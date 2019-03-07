using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Configurations
{
    public interface IEntityConfiguration
    {
        void Map(ModelBuilder builder);
    }
}
