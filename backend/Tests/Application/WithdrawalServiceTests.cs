using ATMWithdrawal.Application.Services;
using ATMWithdrawal.Domain.Exceptions;
using Xunit;

namespace ATMWithdrawal.Tests.Application;

/// <summary>
/// Comprehensive unit tests for the WithdrawalService.
/// Tests cover valid inputs, edge cases, and exception scenarios.
/// </summary>
public class WithdrawalServiceTests
{
    private readonly WithdrawalService _service;

    public WithdrawalServiceTests()
    {
        _service = new WithdrawalService();
    }

    #region Valid Input Tests

    [Fact]
    public async Task WithdrawAsync_Amount30_ReturnsCorrectNotes()
    {
        // Arrange
        decimal amount = 30m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.NoteCount);
        Assert.Equal(30m, result.TotalAmount);
        Assert.Contains(20m, result.Notes);
        Assert.Contains(10m, result.Notes);
    }

    [Fact]
    public async Task WithdrawAsync_Amount80_ReturnsCorrectNotes()
    {
        // Arrange
        decimal amount = 80m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.NoteCount);
        Assert.Equal(80m, result.TotalAmount);
        Assert.Contains(50m, result.Notes);
        Assert.Contains(20m, result.Notes);
        Assert.Contains(10m, result.Notes);
    }

    [Fact]
    public async Task WithdrawAsync_Amount100_ReturnsSingleNote()
    {
        // Arrange
        decimal amount = 100m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Notes);
        Assert.Equal(100m, result.TotalAmount);
        Assert.Contains(100m, result.Notes);
    }

    [Fact]
    public async Task WithdrawAsync_Amount200_ReturnsMinimumNotes()
    {
        // Arrange
        decimal amount = 200m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.NoteCount);
        Assert.Equal(200m, result.TotalAmount);
        Assert.Equal(2, result.Notes.Count(n => n == 100m));
    }

    [Fact]
    public async Task WithdrawAsync_Amount50_ReturnsSingleFiftyNote()
    {
        // Arrange
        decimal amount = 50m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Notes);
        Assert.Equal(50m, result.TotalAmount);
        Assert.Contains(50m, result.Notes);
    }

    [Fact]
    public async Task WithdrawAsync_Amount20_ReturnsSingleTwentyNote()
    {
        // Arrange
        decimal amount = 20m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Notes);
        Assert.Equal(20m, result.TotalAmount);
        Assert.Contains(20m, result.Notes);
    }

    [Fact]
    public async Task WithdrawAsync_Amount10_ReturnsSingleTenNote()
    {
        // Arrange
        decimal amount = 10m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Notes);
        Assert.Equal(10m, result.TotalAmount);
        Assert.Contains(10m, result.Notes);
    }

    [Fact]
    public async Task WithdrawAsync_Amount170_ReturnsOptimalNotes()
    {
        // Arrange
        decimal amount = 170m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.NoteCount); // 100 + 50 + 20 (3 notes)
        Assert.Equal(170m, result.TotalAmount);
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public async Task WithdrawAsync_NullAmount_ReturnsEmptySet()
    {
        // Arrange
        decimal? amount = null;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Notes);
        Assert.Equal(0m, result.TotalAmount);
        Assert.Equal(0, result.NoteCount);
    }

    [Fact]
    public async Task WithdrawAsync_ZeroAmount_ReturnsEmptySet()
    {
        // Arrange
        decimal amount = 0m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Notes);
        Assert.Equal(0m, result.TotalAmount);
        Assert.Equal(0, result.NoteCount);
    }

    [Fact]
    public async Task WithdrawAsync_LargeAmount_ReturnsMinimumNotes()
    {
        // Arrange
        decimal amount = 1000m;

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.NoteCount); // 10 x 100 notes
        Assert.Equal(1000m, result.TotalAmount);
        Assert.All(result.Notes, note => Assert.Equal(100m, note));
    }

    #endregion

    #region Exception Tests

    [Fact]
    public async Task WithdrawAsync_NegativeAmount_ThrowsInvalidArgumentException()
    {
        // Arrange
        decimal amount = -130m;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidArgumentException>(
            () => _service.WithdrawAsync(amount)
        );

        Assert.Contains("negative", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task WithdrawAsync_Amount125_ThrowsNoteUnavailableException()
    {
        // Arrange
        decimal amount = 125m;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NoteUnavailableException>(
            () => _service.WithdrawAsync(amount)
        );

        Assert.Equal(125m, exception.RequestedAmount);
        Assert.Contains("125", exception.Message);
    }

    [Fact]
    public async Task WithdrawAsync_Amount5_ThrowsNoteUnavailableException()
    {
        // Arrange - amount smaller than smallest note
        decimal amount = 5m;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NoteUnavailableException>(
            () => _service.WithdrawAsync(amount)
        );

        Assert.Equal(5m, exception.RequestedAmount);
    }

    [Fact]
    public async Task WithdrawAsync_Amount15_ThrowsNoteUnavailableException()
    {
        // Arrange - amount that cannot be formed with available notes
        decimal amount = 15m;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NoteUnavailableException>(
            () => _service.WithdrawAsync(amount)
        );

        Assert.Equal(15m, exception.RequestedAmount);
    }

    [Fact]
    public async Task WithdrawAsync_Amount25_ThrowsNoteUnavailableException()
    {
        // Arrange
        decimal amount = 25m;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NoteUnavailableException>(
            () => _service.WithdrawAsync(amount)
        );

        Assert.Equal(25m, exception.RequestedAmount);
    }

    [Fact]
    public async Task WithdrawAsync_Amount35_ThrowsNoteUnavailableException()
    {
        // Arrange
        decimal amount = 35m;

        // Act & Assert
        await Assert.ThrowsAsync<NoteUnavailableException>(
            () => _service.WithdrawAsync(amount)
        );
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    [InlineData(-1000)]
    public async Task WithdrawAsync_VariousNegativeAmounts_ThrowsInvalidArgumentException(decimal amount)
    {
        // Act & Assert
        await Assert.ThrowsAsync<InvalidArgumentException>(
            () => _service.WithdrawAsync(amount)
        );
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(7)]
    [InlineData(13)]
    [InlineData(25)]
    [InlineData(35)]
    [InlineData(125)]
    public async Task WithdrawAsync_ImpossibleAmounts_ThrowsNoteUnavailableException(decimal amount)
    {
        // Act & Assert
        await Assert.ThrowsAsync<NoteUnavailableException>(
            () => _service.WithdrawAsync(amount)
        );
    }

    #endregion

    #region Algorithm Verification Tests

    [Theory]
    [InlineData(10, 1)]
    [InlineData(20, 1)]
    [InlineData(30, 2)]
    [InlineData(40, 2)]
    [InlineData(50, 1)]
    [InlineData(60, 2)]
    [InlineData(70, 2)]  // 50 + 20
    [InlineData(80, 3)]
    [InlineData(90, 3)]  // 50 + 20 + 20
    [InlineData(100, 1)]
    [InlineData(110, 2)]
    [InlineData(120, 2)]
    [InlineData(130, 3)]
    [InlineData(140, 3)]
    [InlineData(150, 2)]
    [InlineData(160, 3)]
    [InlineData(170, 3)]  // 100 + 50 + 20
    [InlineData(180, 4)]
    [InlineData(190, 4)]  // 100 + 50 + 20 + 20
    [InlineData(200, 2)]
    public async Task WithdrawAsync_ValidAmounts_ReturnsMinimumNoteCount(decimal amount, int expectedNoteCount)
    {
        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.Equal(expectedNoteCount, result.NoteCount);
        Assert.Equal(amount, result.TotalAmount);
    }

    [Fact]
    public async Task WithdrawAsync_GreedyAlgorithm_UsesLargestNotesFirst()
    {
        // Arrange
        decimal amount = 280m; // Should be 2x100 + 1x50 + 1x20 + 1x10

        // Act
        var result = await _service.WithdrawAsync(amount);

        // Assert
        Assert.Equal(5, result.NoteCount);
        Assert.Equal(280m, result.TotalAmount);
        
        // Verify notes are in descending order (greedy approach)
        var sortedNotes = result.Notes.OrderByDescending(n => n).ToList();
        Assert.Equal(result.Notes, sortedNotes);
    }

    #endregion
}
