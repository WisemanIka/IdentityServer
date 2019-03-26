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
            CreateMap<CreateProductRequest, Products>(MemberList.Source)
                .ForMember(dest => dest.CreatedBy, source => source.MapFrom(s => s.UserId))
                .ForMember(dest => dest.CreatedAt, source => source.MapFrom(s => DateTime.UtcNow))
                .IgnoreAllNonExisting();

            CreateMap<CreateProductRequest, Products>(MemberList.Source)
                .ForMember(dest => dest.UpdatedBy, source => source.MapFrom(s => s.UserId))
                .ForMember(dest => dest.UpdatedAt, source => source.MapFrom(s => DateTime.UtcNow))
                .IgnoreAllNonExisting();
        }
    }
}
