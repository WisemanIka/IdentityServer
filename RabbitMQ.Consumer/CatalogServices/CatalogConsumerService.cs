using System;
using System.Text;
using Fox.Common.Constants;
using Fox.Common.Infrastructure;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer.Interfaces;

namespace RabbitMQ.Consumer.CatalogServices
{
    public class CatalogConsumerService : ICatalogConsumerService, IDisposable
    {
        private readonly IRabbitMqFactory _rabbitMqFactory;
        private IModel _consumerChannel;

        public CatalogConsumerService(IRabbitMqFactory rabbitMqFactory)
        {
            this._rabbitMqFactory = rabbitMqFactory;
        }

        public void ProductRevisionConsumer()
        {
            if (!_rabbitMqFactory.IsConnected)
            {
                _rabbitMqFactory.TryConnect();
            }

            using (var channel = _rabbitMqFactory.CreateModel())
            {
                channel.QueueDeclare(queue: RabbitMqConstants.ProductRevisionQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.Write(message);
                };

                channel.BasicConsume(queue: RabbitMqConstants.ProductRevisionQueue, autoAck: true, consumer: consumer);

                //channel.CallbackException += (sender, ea) =>
                //{
                //    _consumerChannel.Dispose();
                //    _consumerChannel = ProductRevisionConsumer();
                //};
            }
        }

        public void Dispose()
        {
            _rabbitMqFactory?.Dispose();
        }
    }
}
