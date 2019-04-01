using Fox.Catalog.Models;

namespace Fox.Catalog.Infrastructure.Interfaces
{
    public interface IRabbitMqService
    {
        void ProductRevisionSender(Products product);
    }
}
