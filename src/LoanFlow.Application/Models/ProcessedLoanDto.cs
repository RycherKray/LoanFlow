

namespace LoanFlow.Application.Models
{
    public record ProcessedLoanDto(
        Guid Id,
        string CustomerName,
        decimal Amount,
        int TermMonths,
        int Type,           
        string Status,      
        DateTime ProcessedAt
    );
}
