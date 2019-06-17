using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Common.Extensions;
using Fox.Provider.Infrastructure.Interfaces;
using Fox.Provider.Models.ViewModels.Provider;
using Fox.Common.Infrastructure;
using Fox.Common.Models;
using Fox.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fox.Provider.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidationModel]
    //[Authorize]
    public class ProviderController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IProviderService ProviderService;

        public ProviderController(IProviderService providerService, ILogger logger)
        {
            this.ProviderService = providerService;
            this.Logger = logger;
        }

        [HttpGet("GetAll")]
        //[ProducesResponseType(typeof(List<ProviderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetProviderRequest filter)
        {
            try
            {
                var providers = await ProviderService.GetAll(filter);
                return Ok(providers);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Provider.Api");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetById/{id}")]
        //[ProducesResponseType(typeof(ProviderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var provider = await ProviderService.GetById(id);
                return Ok(provider);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Provider.Api");
                return BadRequest(ex);
            }
        }

        [HttpGet("Dictionary")]
        [ProducesResponseType(typeof(List<SimpleDictionary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsDictionary(GetProviderRequest request)
        {
            try
            {
                var providers = await ProviderService.GetAsDictionary(request);
                return Ok(providers);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Provider.Api");
                return BadRequest(ex);
            }
        }

        [HttpPost("Save")]
        //[ProducesResponseType(typeof(ValidationResultModel<ProviderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Save([FromBody]CreateProviderRequest request)
        {
            try
            {
                request.UserId = User.GetUserId();

                var result = await ProviderService.Save(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Provider.Api");
                return BadRequest(ex);
            }
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await ProviderService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Provider.Api");
                return BadRequest(ex);
            }
        }
    }
}