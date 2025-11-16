using ATMWithdrawal.Domain.Exceptions;
using Xunit;

namespace ATMWithdrawal.Tests.Domain;

/// <summary>
/// Tests for domain exceptions.
/// </summary>
public class ExceptionTests
{
    [Fact]
    public void NoteUnavailableException_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        decimal amount = 125m;

        // Act
        var exception = new NoteUnavailableException(amount);

        // Assert
        Assert.Equal(amount, exception.RequestedAmount);
        Assert.Contains("125", exception.Message);
        Assert.Contains("cannot", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void NoteUnavailableException_WithCustomMessage_UsesCustomMessage()
    {
        // Arrange
        decimal amount = 125m;
        string customMessage = "Custom error message";

        // Act
        var exception = new NoteUnavailableException(amount, customMessage);

        // Assert
        Assert.Equal(amount, exception.RequestedAmount);
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void InvalidArgumentException_Constructor_SetsMessageCorrectly()
    {
        // Arrange
        string message = "Invalid argument provided";

        // Act
        var exception = new InvalidArgumentException(message);

        // Assert
        Assert.Equal(message, exception.Message);
    }
}
