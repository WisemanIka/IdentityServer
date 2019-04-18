using System;
using System.Net;
using System.Security.Claims;
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

        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegistrationResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest registration)
        {
            try
            {
                var register = await AccountService.Registration(registration);
                return Ok(register);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api");
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            try
            {
                var loginResponse = await AccountService.Login(request);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api");
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("CheckEmail")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                    return BadRequest();

                var emailExists = await AccountService.CheckEmailExistence(email);
                return Ok(emailExists);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api");
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                var register = await AccountService.ConfirmEmail(userId, token);
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