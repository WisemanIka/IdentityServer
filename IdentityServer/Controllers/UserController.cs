using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationModel]
    public class UserController : ControllerBase
    {
        public readonly IUserService UserService;
        public readonly ILogger Logger;

        public UserController(IUserService userService, ILogger logger)
        {
            this.UserService = userService;
            this.Logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("GetUsers")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public IActionResult GetUsers(List<string> userIds)
        {
            try
            {
                var users = UserService.GetUserData(userIds);
                return Ok(users);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api - User");
                return BadRequest(ex);
            }
        }
    }
}