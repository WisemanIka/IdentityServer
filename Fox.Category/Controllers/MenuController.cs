using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Category.Infrastructure.Interfaces;
using Fox.Category.Models.ViewModels.Menu;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fox.Category.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidationModel]
    public class MenuController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IMenuService MenuService;

        public MenuController(IMenuService menuService, ILogger logger)
        {
            this.MenuService = menuService;
            this.Logger = logger;
        }

        [HttpGet("GetMenu")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<MenuResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var menu = await MenuService.GetAll();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Category.Api - Menu");
                return BadRequest(ex);
            }
        }
    }
}