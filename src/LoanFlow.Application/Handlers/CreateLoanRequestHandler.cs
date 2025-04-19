using MediatR;
using LoanFlow.Domain;
using LoanFlow.Application.Repositories;
using LoanFlow.Application.Services;
using LoanFlow.Application.Commands;  

namespace LoanFlow.Application.Handlers;

public class CreateLoanRequestHandler
    : IRequestHandler<CreateLoanRequestCommand, Guid>
{
    private readonly ILoanRepository _repo;
    private readonly IServiceBusPublisher _sb;

    public CreateLoanRequestHandler(
        ILoanRepository repo,
        IServiceBusPublisher sb)
    {
        _repo = repo;
        _sb = sb;
    }

    public async Task<Guid> Handle(CreateLoanRequestCommand cmd, CancellationToken ct)
    {
        var loan = new LoanRequest
        {
            CustomerName = cmd.CustomerName,
            Amount = cmd.Amount,
            TermMonths = cmd.TermMonths,
            Type = cmd.Type
        };

        await _repo.AddAsync(loan, ct);
        await _sb.SendAsync("loan-processing", new { loan.Id }, ct);

        return loan.Id;
    }
}
