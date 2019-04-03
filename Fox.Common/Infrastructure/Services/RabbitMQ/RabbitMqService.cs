using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Fox.Common.Infrastructure
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly IRabbitMqFactory _rabbitMqFactory;

        public RabbitMqService(IRabbitMqFactory rabbitMqFactory)
        {
            this._rabbitMqFactory = rabbitMqFactory;
        }

        public async Task RabbitMqSender<T>(T model, string queueName)
        {
            if (!_rabbitMqFactory.IsConnected)
            {
                _rabbitMqFactory.TryConnect();
            }

            using (var channel = _rabbitMqFactory.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                var revision = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(revision);
                
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
            }

            //TODO Async
            await Task.Delay(50);
        }

        public void Dispose()
        {
            _rabbitMqFactory?.Dispose();
        }
    }
}
