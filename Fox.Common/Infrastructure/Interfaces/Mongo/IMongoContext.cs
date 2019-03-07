using MongoDB.Driver;

namespace Fox.Common.Infrastructure
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>() where T : class;
    }
}
