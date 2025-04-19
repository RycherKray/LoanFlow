using MediatR;
using Microsoft.AspNetCore.Mvc;
using LoanFlow.Application.Commands;

namespace LoanFlow.Api.Controllers;

[ApiController]
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
}
