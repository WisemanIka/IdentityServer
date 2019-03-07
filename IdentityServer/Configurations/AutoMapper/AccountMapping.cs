using AutoMapper;
using Fox.Common.Extensions;
using IdentityServer.Models;

namespace IdentityServer.Configurations.AutoMapper
{
    public class AccountMapping : BaseMapping
    {
        public AccountMapping()
        {
            CreateMap<RegistrationRequest, User>(MemberList.None)
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.Email, src => src.MapFrom(s => s.Email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(s => s.Email))
                .ForMember(dest => dest.Firstname, src => src.MapFrom(s => s.Firstname))
                .ForMember(dest => dest.Lastname, src => src.MapFrom(s => s.Lastname))
                .ReverseMap()
                .IgnoreAll();
        }
    }
}
