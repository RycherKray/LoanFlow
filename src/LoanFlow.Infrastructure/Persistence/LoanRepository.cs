using LoanFlow.Application.Repositories;
using LoanFlow.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanFlow.Infrastructure.Persistence;

public class LoanRepository : ILoanRepository
{
    private readonly LoanDbContext _db;
    public LoanRepository(LoanDbContext db) => _db = db;

    public async Task AddAsync(LoanRequest loan, CancellationToken ct = default)
    {
        _db.LoanRequests.Add(loan);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<int> CountAsync(CancellationToken ct = default)
        => await _db.LoanRequests.CountAsync(ct);
}
