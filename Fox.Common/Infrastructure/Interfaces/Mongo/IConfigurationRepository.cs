using System.Threading.Tasks;
using Fox.Common.Configurations;

namespace Fox.Common.Infrastructure
{
    public interface IConfigurationRepository
    {
        Task<ConfigurationDocument> ReadConfiguration(string name);
    }
}
