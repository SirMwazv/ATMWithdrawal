namespace ATMWithdrawal.Domain.Exceptions;

/// <summary>
/// Exception thrown when an invalid argument is provided (e.g., negative amount).
/// </summary>
public class InvalidArgumentException : Exception
{
    public InvalidArgumentException(string message) : base(message)
    {
    }

    public InvalidArgumentException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
