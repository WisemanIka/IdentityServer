using MongoDB.Driver;
using RabbitMQ.Client;

namespace Fox.Common.Infrastructure
{
    public interface IRabbitMQContext
    {
        bool TryConnect();
        IModel CreateModel();
        void Disconnect();
        void Dispose();
    }
}
