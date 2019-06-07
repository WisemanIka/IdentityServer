using System.Threading.Tasks;
using Fox.BasketApi.Infrastructure.Interfaces;
using Fox.BasketApi.Models.ViewModels.Basket;
using Fox.Common.Infrastructure;

namespace Fox.BasketApi.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        protected readonly ILogger Logger;
        public BasketService(ILogger logger)
        {
            this.Logger = logger;
        }

        public async Task<BasketResponse> Save(CreateBasketRequest request)
        {
            return new BasketResponse();
        }
    }
}
