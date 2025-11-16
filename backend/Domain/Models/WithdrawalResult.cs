namespace ATMWithdrawal.Domain.Models;

/// <summary>
/// Represents the result of a withdrawal operation containing the dispensed notes.
/// </summary>
public class WithdrawalResult
{
    /// <summary>
    /// List of note denominations dispensed for the withdrawal.
    /// </summary>
    public List<decimal> Notes { get; set; } = new();

    /// <summary>
    /// Total amount withdrawn.
    /// </summary>
    public decimal TotalAmount => Notes.Sum();

    /// <summary>
    /// Total number of notes dispensed.
    /// </summary>
    public int NoteCount => Notes.Count;
}
