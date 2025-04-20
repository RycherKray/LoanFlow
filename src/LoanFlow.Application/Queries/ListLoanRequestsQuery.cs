using System.Collections.Generic;
using LoanFlow.Application.Models;
using MediatR;

namespace LoanFlow.Application.Queries
{
    public record ListLoanRequestsQuery()
        : IRequest<IEnumerable<ProcessedLoanDto>>;
}