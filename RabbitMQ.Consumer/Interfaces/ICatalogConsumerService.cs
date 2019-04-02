using RabbitMQ.Client;

namespace RabbitMQ.Consumer.Interfaces
{
    public interface ICatalogConsumerService
    {
        void ProductRevisionConsumer();
    }
}
