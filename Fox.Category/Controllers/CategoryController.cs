using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Models.ViewModels.Category;
using Fox.Common.Infrastructure;
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
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }
    }
}