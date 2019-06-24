using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationModel]
    public class UserController : ControllerBase
    {
        public readonly IAccountService UserService;
        public readonly ILogger Logger;

        public UserController(IAccountService userService, ILogger logger)
        {
            this.UserService = userService;
            this.Logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("GetUsers")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var register = await UserService.ConfirmEmail("", "");
                return Ok(register);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api");
                return BadRequest(ex);
            }
        }
    }
}