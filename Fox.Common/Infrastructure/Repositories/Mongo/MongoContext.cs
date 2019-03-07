using System;
using Fox.Common.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fox.Common.Infrastructure
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IOptions<MongoSettings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception ex)
            {
                throw new Exception("Mongo Settings", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}
