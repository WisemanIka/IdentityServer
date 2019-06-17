using System;
using AutoMapper;
using Fox.Common.Extensions;
using Fox.Provider.Models;
using Fox.Provider.Models.ViewModels.Provider;

namespace Fox.Provider.Configurations.AutoMapper
{
    public class ProviderMapping : Profile
    {
        public ProviderMapping()
        {
            CreateMap<CreateProviderRequest, Providers>(MemberList.None)
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(s => s.UserId))
                .ForMember(dest => dest.CreatedAt, src => src.UseValue(DateTime.UtcNow));
        }
    }
}
