using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer.Services
{
    public class AmqpMessagingService
    {
        private string _hostName = "localhost";
        private string _userName = "guest";
        private string _password = "guest";
        private string _exchangeName = "";
        private string _oneWayMessageQueueName = "OneWayMessageQueue";
        private string _workerQueueDemoQueueName = "WorkerQueueDemoQueue";
        private bool _durable = true;

        public IConnection GetRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            return connectionFactory.CreateConnection();
        }

        public void SetUpQueueForOneWayMessageDemo(IModel model)
        {
            model.QueueDeclare(_oneWayMessageQueueName, _durable, false, false, null);
        }

        public void SetUpQueueForWorkerQueueDemo(IModel model)
        {
            model.QueueDeclare(_workerQueueDemoQueueName, _durable, false, false, null);
        }

        public void SendOneWayMessage(string message, IModel model)
        {
            var basicProperties = model.CreateBasicProperties();
            basicProperties.Persistent = _durable;

            var messageBytes = Encoding.UTF8.GetBytes(message);
            model.BasicPublish(_exchangeName, _oneWayMessageQueueName, basicProperties, messageBytes);
        }

        public void ReceiveOneWayMessages(IModel model)
        {
            model.BasicQos(0, 1, false); //basic quality of service
            var consumer = new QueueingBasicConsumer(model);
            model.BasicConsume(_oneWayMessageQueueName, false, consumer);
            while (true)
            {
                var deliveryArguments = consumer.Queue.Dequeue() as BasicDeliverEventArgs;
                var message = Encoding.UTF8.GetString(deliveryArguments.Body);
                Console.WriteLine("Message received: {0}", message);
                model.BasicAck(deliveryArguments.DeliveryTag, false);
            }
        }


    }
}
