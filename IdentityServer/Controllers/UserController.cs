using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToArray();
            return Ok(new { message = "Hello API", claims });
        }
    }
}