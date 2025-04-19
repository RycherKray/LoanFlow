using MediatR;
using LoanFlow.Domain;
using LoanFlow.Domain.Enums;

namespace LoanFlow.Application.Commands;

public record CreateLoanRequestCommand(
    string CustomerName,
    decimal Amount,
    int TermMonths,
    LoanType Type
) : IRequest<Guid>;