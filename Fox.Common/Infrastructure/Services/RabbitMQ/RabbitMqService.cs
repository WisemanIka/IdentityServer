using System;
using System.Text;
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

        public void RabbitMqSender<T>(T model, string queueName)
        {
            if (_rabbitMqFactory.IsConnected)
            {
                _rabbitMqFactory.TryConnect();
            }

            using (var channel = _rabbitMqFactory.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                //var properties = channel.CreateBasicProperties();
                //Additional properties

                var product = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(product);

                //channel.ConfirmSelect();

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

                //channel.WaitForConfirmsOrDie();
            }
        }

        public void Dispose()
        {
            _rabbitMqFactory?.Dispose();
        }
    }
}
