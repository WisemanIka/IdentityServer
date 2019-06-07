using AutoMapper;
using Fox.BasketApi.Models;
using Fox.BasketApi.Models.ViewModels.Basket;
using Fox.Common.Extensions;
using System;

namespace Fox.BasketApi.Configurations.AutoMapper
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<CreateBasketRequest, Basket>(MemberList.None)
                .IgnoreAllNonExisting();
        }
    }
}
