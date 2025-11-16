namespace ATMWithdrawal.API.Models;

/// <summary>
/// Response model for withdrawal operations.
/// </summary>
public class WithdrawalResponse
{
    /// <summary>
    /// List of notes dispensed.
    /// </summary>
    public List<decimal> Notes { get; set; } = new();

    /// <summary>
    /// Total amount dispensed.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Total number of notes dispensed.
    /// </summary>
    public int NoteCount { get; set; }
}
