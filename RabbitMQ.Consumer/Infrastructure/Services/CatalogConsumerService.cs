using System;
using System.Text;
using System.Threading.Tasks;
using Fox.Common.Constants;
using Fox.Common.Infrastructure;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer.Infrastructure.Interfaces;
using RabbitMQ.Consumer.Models;

namespace RabbitMQ.Consumer.Infrastructure.Services
{
    public class CatalogConsumerService : ICatalogConsumerService, IDisposable
    {
        private readonly IMongoContext _context;
        private readonly IRabbitMqFactory _rabbitMqFactory;
        private IModel _consumerChannel;

        public CatalogConsumerService(IRabbitMqFactory rabbitMqFactory, IMongoContext ctx)
        {
            this._rabbitMqFactory = rabbitMqFactory;
            this._context = ctx;
        }

        public void ProductRevisionConsumer()
        {
            if (!_rabbitMqFactory.IsConnected)
            {
                _rabbitMqFactory.TryConnect();
            }

            var channel = _rabbitMqFactory.CreateModel();

            channel.QueueDeclare(queue: RabbitMqConstants.ProductRevisionQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += SaveProductRevision;

            channel.BasicConsume(queue: RabbitMqConstants.ProductRevisionQueue, autoAck: true, consumer: consumer);

            //channel.CallbackException += (sender, ea) =>
            //{
            //    _consumerChannel.Dispose();
            //    _consumerChannel = ProductRevisionConsumer();
            //};
        }

        private async Task SaveProductRevision(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            var revision = JsonConvert.DeserializeObject<ProductRevisions>(message);

            await _context.GetCollection<ProductRevisions>().InsertOneAsync(revision);
        }

        public void Dispose()
        {
            _rabbitMqFactory?.Dispose();
        }
    }
}
