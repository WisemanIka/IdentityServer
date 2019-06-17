using System;
using AutoMapper;
using Fox.BasketApi.Models;
using Fox.BasketApi.Models.ViewModels.Review;
using Fox.Common.Extensions;

namespace Fox.BasketApi.Configurations.AutoMapper
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<CreateReviewRequest, Reviews>(MemberList.None)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(s => s.UserId))
                .ForMember(dest => dest.CreatedAt, src => src.UseValue(DateTime.UtcNow));
        }
    }
}
