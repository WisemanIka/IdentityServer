using AutoMapper;
using Fox.Common.Extensions;
using RabbitMQ.Consumer.Models;

namespace RabbitMQ.Consumer.Configurations.AutoMapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductResponse, Products>(MemberList.None)
                .IgnoreAllNonExisting();
        }
    }
}
