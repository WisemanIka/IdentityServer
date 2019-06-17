using System;
using System.Net;
using System.Threading.Tasks;
using Fox.BasketApi.Infrastructure.Interfaces;
using Fox.BasketApi.Models.ViewModels.Basket;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Fox.BasketApi.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IBasketService BasketService;

        public BasketController(IBasketService basketService, ILogger logger)
        {
            this.BasketService = basketService;
            this.Logger = logger;
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(BasketResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                //var basket = await BasketService.GetById(id);
                return Ok(null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Basket.Api");
                return BadRequest(ex);
            }
        }

        [HttpPost("Save")]
        [ProducesResponseType(typeof(BasketResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Save([FromBody]CreateBasketRequest request)
        {
            try
            {
                request.UserId = User.GetUserId();

                var result = await BasketService.Save(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Basket.Api");
                return BadRequest(ex);
            }
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                //var result = await BasketService.Delete(id);
                return Ok(null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Basket.Api");
                return BadRequest(ex);
            }
        }
    }
}