using System;
using RabbitMQ.Client;
using RabbitMQ.Consumer.Services;

namespace OneWayMessageSender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var messagingService = new AmqpMessagingService();
            var connection = messagingService.GetRabbitMqConnection();
            var model = connection.CreateModel();

            RunOneWayMessageDemo(model, messagingService);

            Console.ReadKey();
            //messagingService.SetUpQueueForOneWayMessageDemo(model);
        }

        private static void RunOneWayMessageDemo(IModel model, AmqpMessagingService messagingService)
        {
            Console.WriteLine("Enter your message and press Enter. Quit with 'q'.");
            while (true)
            {
                string message = Console.ReadLine();
                if (message.ToLower() == "q") break;

                messagingService.SendOneWayMessage(message, model);
            }
        }
    }
}
