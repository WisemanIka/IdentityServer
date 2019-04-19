using System;
using AutoMapper;
using Fox.Category.Models;
using Fox.Category.Models.ViewModels.Category;
using Fox.Common.Extensions;

namespace Fox.Category.Configurations.AutoMapper
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CreateCategoryRequest, Categories>(MemberList.None)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(s => s.UserId))
                .ForMember(dest => dest.CreatedAt, src => src.UseValue(DateTime.UtcNow));
        }
    }
}
