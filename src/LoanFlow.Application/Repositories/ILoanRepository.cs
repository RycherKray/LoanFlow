using LoanFlow.Domain;

namespace LoanFlow.Application.Repositories;

public interface ILoanRepository
{
    Task AddAsync(LoanRequest loan, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
