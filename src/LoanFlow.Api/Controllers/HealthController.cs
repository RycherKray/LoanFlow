using LoanFlow.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("db")]
        public async Task<IActionResult> Count([FromServices] ILoanRepository repo)
        => Ok(await repo.CountAsync());
    }
}
