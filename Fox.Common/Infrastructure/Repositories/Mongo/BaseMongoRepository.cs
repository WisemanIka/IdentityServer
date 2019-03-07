using System;

namespace Fox.Common.Infrastructure
{
    public class BaseMongoRepository : IDisposable
    {
        public IMongoContext Ctx { get; private set; }

        public BaseMongoRepository(IMongoContext ctx)
        {
            this.Ctx = ctx;
        }

        public BaseMongoRepository()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
