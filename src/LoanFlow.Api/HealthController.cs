using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanFlow.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet] 
        public IActionResult Ping() => Ok("pong");
    }
}
