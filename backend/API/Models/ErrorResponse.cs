namespace ATMWithdrawal.API.Models;

/// <summary>
/// Standard error response model.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Error message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Error type/code.
    /// </summary>
    public string ErrorType { get; set; } = string.Empty;

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public int StatusCode { get; set; }
}
