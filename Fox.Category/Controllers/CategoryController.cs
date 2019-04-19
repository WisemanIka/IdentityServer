using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Models.ViewModels.Category;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Models;
using Fox.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fox.Category.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidationModel]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly ICategoryService CategoryService;

        public CategoryController(ICategoryService categoryService, ILogger logger)
        {
            this.CategoryService = categoryService;
            this.Logger = logger;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetCategoryRequest request)
        {
            try
            {
                var category = await CategoryService.GetAll(request);
                return Ok(category);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Category.Api");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var category = await CategoryService.GetById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Category.Api");
                return BadRequest(ex);
            }
        }

        [HttpGet("Dictionary")]
        [ProducesResponseType(typeof(List<SimpleDictionary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsDictionary(GetCategoryRequest filter)
        {
            try
            {
                var categories = await CategoryService.GetAsDictionary(filter);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Category.Api");
                return BadRequest(ex);
            }
        }

        [HttpPost("Save")]
        [ProducesResponseType(typeof(ValidationResultModel<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Save([FromBody]CreateCategoryRequest request)
        {
            try
            {
                request.UserId = User.GetUserId();

                var result = await CategoryService.Save(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Category.Api");
                return BadRequest(ex);
            }
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await CategoryService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Category.Api");
                return BadRequest(ex);
            }
        }
    }
}