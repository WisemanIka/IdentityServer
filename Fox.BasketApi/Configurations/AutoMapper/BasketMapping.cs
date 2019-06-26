using AutoMapper;
using Fox.Basket.Models;
using Fox.Basket.Models.ViewModels.Basket;
using Fox.Common.Extensions;

namespace Fox.Basket.Configurations.AutoMapper
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<CreateBasketRequest, Baskets>(MemberList.None)
                .IgnoreAllNonExisting();
        }
    }
}
