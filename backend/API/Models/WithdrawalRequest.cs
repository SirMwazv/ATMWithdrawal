namespace ATMWithdrawal.API.Models;

/// <summary>
/// Request model for withdrawal operations.
/// </summary>
public class WithdrawalRequest
{
    /// <summary>
    /// The amount to withdraw.
    /// </summary>
    /// <example>30.00</example>
    public decimal? Amount { get; set; }
}
