
namespace Fox.Common.Infrastructure
{
    public class BaseMongoService
    {
        protected readonly IMongoContext Ctx;
        protected readonly ILogger Logger;

        public BaseMongoService(ILogger logger, IMongoContext ctx)
        {
            this.Ctx = ctx;
            this.Logger = logger;
        }
    }
}
