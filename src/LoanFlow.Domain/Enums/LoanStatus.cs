namespace LoanFlow.Domain;

public enum LoanStatus
{
    Submitted,    // just received
    Processing,   // underwriting / document scans in flight
    Approved,     // ready to fund
    Declined      // rejected
}