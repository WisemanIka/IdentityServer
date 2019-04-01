using System;
using RabbitMQ.Client;
using RabbitMQ.Consumer.Services;

namespace OneWayMessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var messagingService = new AmqpMessagingService();
            var connection = messagingService.GetRabbitMqConnection();
            var model = connection.CreateModel();
            messagingService.ReceiveOneWayMessages(model);
        }
    }
}
