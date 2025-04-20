using LoanFlow.Application.Models;

namespace LoanFlow.Application.Repositories
{
    public interface IProcessedLoanRepository
    {
        Task<IEnumerable<ProcessedLoanDto>> GetAllAsync();
        Task<ProcessedLoanDto?> GetByIdAsync(Guid id);
    }
}
