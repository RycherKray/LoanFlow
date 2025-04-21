

namespace LoanFlow.Application.Models
{
    public record ProcessedLoanDto(
        Guid Id,
        string CustomerName,
        decimal Amount,
        int TermMonths,
        string Type,           
        string Status,      
        DateTime ProcessedAt
    );
}
