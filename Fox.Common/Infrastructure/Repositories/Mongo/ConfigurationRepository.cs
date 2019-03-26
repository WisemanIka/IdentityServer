using System.Threading.Tasks;
using Fox.Common.Configurations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fox.Common.Infrastructure
{
    public class ConfigurationRepository : BaseMongoRepository, IConfigurationRepository
    {
        private readonly IMongoContext _ctx;

        public ConfigurationRepository(IMongoContext ctx) : base(ctx)
        {
            this._ctx = ctx;
        }

        public async Task<ConfigurationDocument> ReadConfiguration(string environment)
        {
            var collection = _ctx.GetCollection<Configuration>().AsQueryable();
            var result = await collection.Where(x => x.Environment == environment).FirstOrDefaultAsync();
            return result?.ConfigurationDocument;
        }
    }
}
