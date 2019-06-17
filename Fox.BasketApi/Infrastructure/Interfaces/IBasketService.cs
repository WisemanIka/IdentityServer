using System.Threading.Tasks;
using Fox.BasketApi.Models.ViewModels.Basket;

namespace Fox.BasketApi.Infrastructure.Interfaces
{
    public interface IBasketService
    {
        Task<BasketResponse> Save(CreateBasketRequest request);
    }
}
