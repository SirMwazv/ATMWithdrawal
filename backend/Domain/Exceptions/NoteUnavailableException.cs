namespace ATMWithdrawal.Domain.Exceptions;

/// <summary>
/// Exception thrown when the requested withdrawal amount cannot be formed with available notes.
/// </summary>
public class NoteUnavailableException : Exception
{
    public decimal RequestedAmount { get; }

    public NoteUnavailableException(decimal amount)
        : base($"Cannot dispense {amount:C}. The amount cannot be formed with available notes (100, 50, 20, 10).")
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
}
