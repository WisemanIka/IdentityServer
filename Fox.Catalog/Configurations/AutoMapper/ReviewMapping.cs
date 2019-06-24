using System;
using AutoMapper;
using Fox.Catalog.Models;
using Fox.Catalog.Models.ViewModels.Review;
using Fox.Common.Extensions;

namespace Fox.Catalog.Configurations.AutoMapper
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<CreateReviewRequest, Reviews>(MemberList.None)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(s => s.UserId))
                .ForMember(dest => dest.CreatedAt, src => src.UseValue(DateTime.UtcNow));


            CreateMap<Reviews, ReviewResponse>(MemberList.None)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.CatalogId, src => src.MapFrom(s => s.CatalogId));
        }
    }
}
