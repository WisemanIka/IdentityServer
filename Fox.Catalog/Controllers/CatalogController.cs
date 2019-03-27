using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models.ViewModels.Product;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fox.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationModel]
    //[Authorize]
    public class CatalogController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IProductService ProductService;

        public CatalogController(IProductService productService, ILogger logger)
        {
            this.ProductService = productService;
            this.Logger = logger;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetProductRequest request)
        {
            try
            {
                var products = await ProductService.GetAll(request);
                return Ok(products);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var product = await ProductService.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }

        [HttpPost("Save")]
        [ProducesResponseType(typeof(ValidationResultModel<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Save([FromBody]CreateProductRequest request)
        {
            try
            {
                request.UserId = User.GetUserId();

                var result = await ProductService.Save(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await ProductService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }
    }
}