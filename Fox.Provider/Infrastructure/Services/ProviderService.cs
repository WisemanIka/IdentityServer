using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Models;
using Fox.Common.Responses;
using Fox.Provider.Infrastructure.Interfaces;
using Fox.Provider.Models;
using Fox.Provider.Models.ViewModels.Provider;

namespace Fox.Provider.Infrastructure.Services
{
    public class ProviderService : BaseMongoService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        public ProviderService(ILogger logger, IMongoContext ctx,
            IProviderRepository providerRepository) : base(logger, ctx)
        {
            this._providerRepository = providerRepository;
        }

        private async Task<IEnumerable<ProviderResponse>> GetProviders(GetProviderRequest filter)
        {
            var providers = await _providerRepository.GetProviders(filter);
            var result = providers.Map<Providers, ProviderResponse>();
            return result;
        }

        public async Task<List<ProviderResponse>> GetAll(GetProviderRequest filter)
        {
            var result = (await GetProviders(filter)).ToList();
            return result;
        }

        public async Task<ProviderResponse> GetById(string id)
        {
            var filter = new GetProviderRequest { Id = id };
            var result = (await GetProviders(filter)).SingleOrDefault();
            return result;
        }

        public async Task<List<SimpleDictionary>> GetAsDictionary(GetProviderRequest filter)
        {
            var result = (await GetProviders(filter)).ToSimpleDictionary();
            return result;
        }

        public async Task<ValidationResultModel<ProviderResponse>> Save(CreateProviderRequest model)
        {
            var result = new ValidationResultModel<ProviderResponse>();

            if (result.Succeeded)
            {
                var providerDbModel = model.Map<CreateProviderRequest, Providers>();
                providerDbModel = await _providerRepository.Save(providerDbModel);
                result.Model = providerDbModel.Map<Providers, ProviderResponse>();
            }

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var provider = (await _providerRepository.GetProviders(new GetProviderRequest { Id = id })).SingleOrDefault();

            if (provider == null)
                return false;

            provider.IsDeleted = true;
            provider.CreatedAt = DateTime.UtcNow;

            await _providerRepository.Save(provider);

            return true;
        }
    }
}
