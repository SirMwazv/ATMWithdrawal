using ATMWithdrawal.Domain.Exceptions;
using ATMWithdrawal.Domain.Models;

namespace ATMWithdrawal.Application.Interfaces;

/// <summary>
/// Service interface for ATM withdrawal operations.
/// </summary>
public interface IWithdrawalService
{
    /// <summary>
    /// Calculates and returns the minimum number of notes needed for the requested amount.
    /// </summary>
    /// <param name="amount">The amount to withdraw.</param>
    /// <returns>A WithdrawalResult containing the list of notes.</returns>
    /// <exception cref="InvalidArgumentException">Thrown when the amount is negative.</exception>
    /// <exception cref="NoteUnavailableException">Thrown when the amount cannot be formed with available notes.</exception>
    Task<WithdrawalResult> WithdrawAsync(decimal? amount);
}
