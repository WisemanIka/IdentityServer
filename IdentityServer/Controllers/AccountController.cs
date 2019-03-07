using System;
using System.Threading.Tasks;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using IdentityServer.Infrastructure;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationModel]
    public class AccountController : ControllerBase
    {
        public readonly IAccountService AccountService;
        public readonly ILogger Logger;

        public AccountController(IAccountService userService, ILogger logger)
        {
            this.AccountService = userService;
            this.Logger = logger;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest registration)
        {
            try
            {
                var register = await AccountService.Registration(registration);
                return Ok(register);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Bom.Identity.Api");
                return BadRequest(ex);
            }
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                var register = await AccountService.Registration(registration);
                return Ok(register);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Bom.Identity.Api");
                return BadRequest(ex);
            }
        }
    }
}