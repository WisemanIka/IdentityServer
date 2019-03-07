using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Configurations
{
    public interface IEntityMappingConfiguration<T> : IEntityConfiguration
        where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}
