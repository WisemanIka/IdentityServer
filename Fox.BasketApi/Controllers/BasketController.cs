using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fox.BasketApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class BasketController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToArray();
            return Ok(new { message = "Hello API Bliad 2", claims });
        }
    }
}