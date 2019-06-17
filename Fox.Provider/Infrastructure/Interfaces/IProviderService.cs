using System.Collections.Generic;
using System.Threading.Tasks;
using Fox.Common.Models;
using Fox.Common.Responses;
using Fox.Provider.Models.ViewModels.Provider;

namespace Fox.Provider.Infrastructure.Interfaces
{
    public interface IProviderService
    {
        Task<List<ProviderResponse>> GetAll(GetProviderRequest filter);
        Task<ProviderResponse> GetById(string id);
        Task<List<SimpleDictionary>> GetAsDictionary(GetProviderRequest filter);
        Task<ValidationResultModel<ProviderResponse>> Save(CreateProviderRequest model);
        Task<bool> Delete(string Id);
    }
}
