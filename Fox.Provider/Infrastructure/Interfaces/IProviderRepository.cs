using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Provider.Models;
using Fox.Provider.Models.ViewModels.Provider;

namespace Fox.Provider.Infrastructure.Interfaces
{
    public interface IProviderRepository
    {
        Task<List<Providers>> GetProviders(GetProviderRequest filter);
        Task<Providers> Save(Providers model);
    }
}
