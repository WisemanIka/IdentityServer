namespace Fox.Common.Infrastructure
{
    public interface IRabbitMqService
    {
        void RabbitMqSender<T>(T model, string queueName);
    }
}
