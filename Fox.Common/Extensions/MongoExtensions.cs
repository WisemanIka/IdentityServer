using Fox.Common.Models;
using MongoDB.Driver.Linq;

namespace Fox.Common.Extensions
{
    public static class MongoExtensions
    {
        public static IMongoQueryable<T> BaseFilter<T>(this IMongoQueryable<T> queryable) where T : BaseMongoCollection
        {
            queryable = queryable.Where(x => x.IsDeleted == null || x.IsDeleted == false);
            return queryable;
        }
    }
}
