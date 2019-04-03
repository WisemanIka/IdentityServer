using System;
using AutoMapper;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Product;
using Fox.Common.Extensions;

namespace Fox.Catalog.Configurations.AutoMapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<CreateProductRequest, Products>(MemberList.None)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(s => s.UserId))
                .ForMember(dest => dest.CreatedAt, src => src.UseValue(DateTime.UtcNow));
        }
    }
}
