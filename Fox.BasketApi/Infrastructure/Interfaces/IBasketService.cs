using System.Threading.Tasks;
using Fox.Basket.Models.ViewModels.Basket;

namespace Fox.Basket.Infrastructure.Interfaces
{
    public interface IBasketService
    {
        Task<BasketResponse> Save(CreateBasketRequest request);
    }
}
