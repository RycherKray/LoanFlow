using LoanFlow.Domain.Enums;

namespace LoanFlow.Domain;

public sealed class LoanRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string CustomerName { get; set; } = default!;
    public decimal Amount { get; set; }
    public int TermMonths { get; set; }
    public LoanType Type { get; set; }
    public LoanStatus Status { get; private set; } = LoanStatus.Submitted;
    public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;

    // Domain behaviour example
    public void Approve() => Status = LoanStatus.Approved;
    public void Decline() => Status = LoanStatus.Declined;
}
