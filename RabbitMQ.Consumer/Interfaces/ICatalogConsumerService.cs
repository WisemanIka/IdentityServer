using RabbitMQ.Client;

namespace RabbitMQ.Consumer.Interfaces
{
    public interface ICatalogConsumerService
    {
        IModel ProductRevisionConsumer();
    }
}
