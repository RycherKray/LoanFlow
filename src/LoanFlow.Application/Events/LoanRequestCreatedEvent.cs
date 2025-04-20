using LoanFlow.Domain.Enums;
namespace LoanFlow.Application.Events
{
    public class LoanRequestCreatedEvent
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public decimal Amount { get; set; }
        public int TermMonths { get; set; }
        public LoanType Type { get; set; }
    }
}
