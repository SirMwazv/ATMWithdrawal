using ATMWithdrawal.Application.Interfaces;
using ATMWithdrawal.Domain.Exceptions;
using ATMWithdrawal.Domain.Models;

namespace ATMWithdrawal.Application.Services;

/// <summary>
/// Service that handles ATM withdrawal operations using a greedy algorithm
/// to dispense the minimum number of notes.
/// </summary>
public class WithdrawalService : IWithdrawalService
{
    // Available note denominations in descending order for greedy algorithm
    private static readonly decimal[] AvailableNotes = { 100m, 50m, 20m, 10m };

    /// <summary>
    /// Calculates and returns the minimum number of notes needed for the requested amount.
    /// Uses a greedy algorithm that selects the largest possible note at each step.
    /// </summary>
    /// <param name="amount">The amount to withdraw (nullable).</param>
    /// <returns>A WithdrawalResult containing the optimal note distribution.</returns>
    /// <exception cref="InvalidArgumentException">Thrown when amount is negative.</exception>
    /// <exception cref="NoteUnavailableException">Thrown when amount cannot be formed with available notes.</exception>
    public Task<WithdrawalResult> WithdrawAsync(decimal? amount)
    {
        // Handle null/empty case - return empty set
        if (!amount.HasValue || amount.Value == 0)
        {
            return Task.FromResult(new WithdrawalResult());
        }

        var requestedAmount = amount.Value;

        // Validate: amount must not be negative
        if (requestedAmount < 0)
        {
            throw new InvalidArgumentException($"Amount cannot be negative. Provided: {requestedAmount:C}");
        }

        // Calculate notes using greedy algorithm
        var notes = CalculateNotes(requestedAmount);

        var result = new WithdrawalResult { Notes = notes };
        return Task.FromResult(result);
    }

    /// <summary>
    /// Implements the greedy algorithm to calculate the minimum number of notes.
    /// Iterates through notes in descending order, using as many of each note as possible.
    /// </summary>
    /// <param name="amount">The amount to dispense.</param>
    /// <returns>List of note denominations.</returns>
    /// <exception cref="NoteUnavailableException">Thrown when exact amount cannot be formed.</exception>
    private static List<decimal> CalculateNotes(decimal amount)
    {
        var notes = new List<decimal>();
        var remaining = amount;

        // Greedy approach: use largest notes first
        foreach (var note in AvailableNotes)
        {
            while (remaining >= note)
            {
                notes.Add(note);
                remaining -= note;
            }
        }

        // If there's any remaining amount, it means we cannot form the exact amount
        // with available notes (e.g., 125 cannot be formed with 100, 50, 20, 10)
        if (remaining > 0)
        {
            throw new NoteUnavailableException(amount);
        }

        return notes;
    }
}
