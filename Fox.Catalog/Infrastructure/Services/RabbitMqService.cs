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
        private readonly IRabbitMQContext _ctx;


        public RabbitMqService(IRabbitMQContext ctx)
        {
            this._ctx = ctx;
        }

        public void ProductRevisionSender(Products product)
        {
            using (var channel = _ctx.CreateModel())
            {
                channel.QueueDeclare(queue: "productRevisionQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var properties = channel.CreateBasicProperties();
                //Additional properties

                var productBody = JsonConvert.SerializeObject(product);
                var body = Encoding.UTF8.GetBytes(productBody);

                channel.ConfirmSelect();

                channel.BasicPublish(exchange: "", routingKey: "userInsertMsgQ", basicProperties: properties, body: body);

                channel.WaitForConfirmsOrDie();
            }
        }
    }
}
