namespace ATMWithdrawal.API.Config;

/// <summary>
/// Currency configuration for the ATM application.
/// Change these values to update the currency throughout the app.
/// </summary>
public static class CurrencyConfig
{
    /// <summary>
    /// Currency symbol (e.g., "R", "$", "€").
    /// </summary>
    public const string Symbol = "R";

    /// <summary>
    /// Currency name (e.g., "Rands", "Dollars", "Euros").
    /// </summary>
    public const string Name = "Rands";

    /// <summary>
    /// Currency code (e.g., "ZAR", "USD", "EUR").
    /// </summary>
    public const string Code = "ZAR";

    /// <summary>
    /// Format amount as currency string.
    /// </summary>
    /// <param name="amount">The amount to format.</param>
    /// <returns>Formatted currency string.</returns>
    public static string FormatCurrency(decimal amount)
    {
        return $"{Symbol}{amount:F2}";
    }
}
