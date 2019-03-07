using Fox.Common.Infrastructure;

namespace IdentityServer.Infrastructure
{
    public class BaseService
    {
        protected readonly IdentityContext Ctx;
        protected readonly ILogger Logger;

        public BaseService(ILogger logger, IdentityContext ctx)
        {
            this.Ctx = ctx;
            this.Logger = logger;
        }
    }
}
