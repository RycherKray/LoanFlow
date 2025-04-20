// LoanFlow.Application/Handlers/ListLoanRequestsHandler.cs
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LoanFlow.Application.Models;
using LoanFlow.Application.Queries;
using LoanFlow.Application.Repositories;

namespace LoanFlow.Application.Handlers
{
    public class ListLoanRequestsHandler
        : IRequestHandler<ListLoanRequestsQuery, IEnumerable<ProcessedLoanDto>>
    {
        private readonly IProcessedLoanRepository _repo;
        public ListLoanRequestsHandler(IProcessedLoanRepository repo)
            => _repo = repo;

        public Task<IEnumerable<ProcessedLoanDto>> Handle(
            ListLoanRequestsQuery req,
            CancellationToken ct)
            => _repo.GetAllAsync();
    }
}
