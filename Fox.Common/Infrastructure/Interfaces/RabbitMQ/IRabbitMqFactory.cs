using MongoDB.Driver;
using RabbitMQ.Client;

namespace Fox.Common.Infrastructure
{
    public interface IRabbitMqFactory
    {
        bool TryConnect();
        IModel CreateModel();
        bool IsConnected { get; }
        void Disconnect();
        void Dispose();
    }
}
