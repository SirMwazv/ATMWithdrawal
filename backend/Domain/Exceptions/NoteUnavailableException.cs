namespace ATMWithdrawal.Domain.Exceptions;

/// <summary>
/// Exception thrown when the requested withdrawal amount cannot be formed with available notes.
/// </summary>
public class NoteUnavailableException : Exception
{
    public decimal RequestedAmount { get; }

    public NoteUnavailableException(decimal amount)
        : base(FormatMessage(amount))
    {
        RequestedAmount = amount;
    }

    public NoteUnavailableException(decimal amount, string message) : base(message)
    {
        RequestedAmount = amount;
    }

    public NoteUnavailableException(decimal amount, string message, Exception innerException)
        : base(message, innerException)
    {
        RequestedAmount = amount;
    }

    private static string FormatMessage(decimal amount)
    {
        // Default currency symbol is R for Rands
        // This can be made configurable via dependency injection if needed
        return $"Cannot dispense R{amount:F2}. The amount cannot be formed with available notes (100, 50, 20, 10).";
    }
}
