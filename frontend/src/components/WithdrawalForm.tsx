import { useState } from 'react';
import { withdrawalApi, WithdrawalResponse, ErrorResponse } from '../api/withdrawalApi';
import './WithdrawalForm.css';

/**
 * Main component for ATM withdrawal interface
 * Handles user input, API calls, and result display
 */
export function WithdrawalForm() {
  const [amount, setAmount] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(false);
  const [result, setResult] = useState<WithdrawalResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  /**
   * Handle form submission
   */
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Clear previous results
    setResult(null);
    setError(null);

    // Validate input
    if (amount.trim() === '') {
      setError('Please enter an amount');
      return;
    }

    const numericAmount = parseFloat(amount);
    
    if (isNaN(numericAmount)) {
      setError('Please enter a valid number');
      return;
    }

    setLoading(true);

    try {
      const response = await withdrawalApi.withdraw(numericAmount);
      setResult(response);
    } catch (err) {
      const errorResponse = err as ErrorResponse;
      setError(errorResponse.message || 'An unexpected error occurred');
    } finally {
      setLoading(false);
    }
  };

  /**
   * Format notes for display
   */
  const formatNotes = (notes: number[]): string => {
    if (notes.length === 0) {
      return 'No notes';
    }
    return notes.map(note => `$${note.toFixed(2)}`).join(' + ');
  };

  /**
   * Count occurrences of each note
   */
  const getNoteBreakdown = (notes: number[]): Map<number, number> => {
    const breakdown = new Map<number, number>();
    notes.forEach(note => {
      breakdown.set(note, (breakdown.get(note) || 0) + 1);
    });
    return breakdown;
  };

  return (
    <div className="withdrawal-container">
      <div className="withdrawal-card">
        <div className="card-header">
          <h1>ATM Withdrawal</h1>
          <p className="subtitle">Enter amount to withdraw</p>
        </div>

        <form onSubmit={handleSubmit} className="withdrawal-form">
          <div className="input-group">
            <span className="currency-symbol">$</span>
            <input
              type="text"
              inputMode="decimal"
              value={amount}
              onChange={(e) => setAmount(e.target.value)}
              placeholder="0.00"
              className="amount-input"
              disabled={loading}
            />
          </div>

          <button 
            type="submit" 
            className="submit-button"
            disabled={loading}
          >
            {loading ? (
              <span className="loading-spinner"></span>
            ) : (
              'Withdraw'
            )}
          </button>
        </form>

        {/* Results Display */}
        {result && (
          <div className="result-panel success">
            <div className="result-header">
              <svg className="icon success-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
              </svg>
              <h2>Withdrawal Successful</h2>
            </div>

            <div className="result-content">
              <div className="total-amount">
                <span className="label">Total Amount:</span>
                <span className="value">${result.totalAmount.toFixed(2)}</span>
              </div>

              <div className="notes-summary">
                <span className="label">Notes ({result.noteCount}):</span>
                <div className="notes-formula">{formatNotes(result.notes)}</div>
              </div>

              <div className="notes-breakdown">
                {Array.from(getNoteBreakdown(result.notes))
                  .sort(([a], [b]) => b - a)
                  .map(([note, count]) => (
                    <div key={note} className="note-item">
                      <div className="note-visual">
                        <span className="note-count">×{count}</span>
                        <span className="note-value">${note.toFixed(0)}</span>
                      </div>
                    </div>
                  ))}
              </div>
            </div>
          </div>
        )}

        {/* Error Display */}
        {error && (
          <div className="result-panel error">
            <div className="result-header">
              <svg className="icon error-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
              </svg>
              <h2>Error</h2>
            </div>
            <div className="result-content">
              <p className="error-message">{error}</p>
            </div>
          </div>
        )}

        <div className="info-section">
          <p className="info-text">Available notes: $100, $50, $20, $10</p>
        </div>
      </div>
    </div>
  );
}
