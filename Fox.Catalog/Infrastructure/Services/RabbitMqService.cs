using System.Text;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models;
using Fox.Common.Infrastructure;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Fox.Catalog.Infrastructure.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IRabbitMqFactory _rabbitMqFactory;
        
        public RabbitMqService(IRabbitMqFactory rabbitMqFactory)
        {
            this._rabbitMqFactory = rabbitMqFactory;
        }

        public void ProductRevisionSender(Products product)
        {
            using (var channel = _rabbitMqFactory.CreateModel())
            {
                channel.QueueDeclare(queue: "productRevisionQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                //var properties = channel.CreateBasicProperties();
                //Additional properties

                var productBody = JsonConvert.SerializeObject(product);
                var body = Encoding.UTF8.GetBytes(productBody);

                //channel.ConfirmSelect();

                channel.BasicPublish(exchange: "", routingKey: "productRevisionQueue", basicProperties: null, body: body);

                //channel.WaitForConfirmsOrDie();
            }
        }
    }
}
