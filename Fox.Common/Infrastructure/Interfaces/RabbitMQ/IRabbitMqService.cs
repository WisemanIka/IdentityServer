using System.Threading.Tasks;

namespace Fox.Common.Infrastructure
{
    public interface IRabbitMqService
    {
        Task RabbitMqSender<T>(T model, string queueName);
    }
}
