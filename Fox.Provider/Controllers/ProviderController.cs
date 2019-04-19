using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fox.Provider.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidationModel]
    public class ProviderController : ControllerBase
    {
        public readonly ILogger Logger;
        //public readonly IProviderService ProviderService;

        //public ProviderController(IProviderService providerService, ILogger logger)
        //{
        //    this.ProviderService = providerService;
        //    this.Logger = logger;
        //}
    }
}