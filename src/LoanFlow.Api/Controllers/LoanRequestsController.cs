using MediatR;
using Microsoft.AspNetCore.Mvc;
using LoanFlow.Application.Commands;
using LoanFlow.Application.Queries;
using LoanFlow.Application.Models;
using Microsoft.AspNetCore.Authorization;

namespace LoanFlow.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/loans")]
public sealed class LoanRequestsController : ControllerBase
{
    private readonly IMediator _mediator;
    public LoanRequestsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Post(CreateLoanRequestCommand cmd)
    {
        var id = await _mediator.Send(cmd);
        return CreatedAtAction(null, new { id }, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProcessedLoanDto>>> Get()
         => Ok(await _mediator.Send(new ListLoanRequestsQuery()));
}
