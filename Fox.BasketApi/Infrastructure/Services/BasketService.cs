using System.Threading.Tasks;
using Fox.Basket.Infrastructure.Interfaces;
using Fox.Basket.Models.ViewModels.Basket;
using Fox.Common.Infrastructure;

namespace Fox.Basket.Infrastructure.Services
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
