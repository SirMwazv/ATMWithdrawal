using ATMWithdrawal.API.Models;
using ATMWithdrawal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ATMWithdrawal.API.Controllers;

/// <summary>
/// Controller for ATM withdrawal operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WithdrawalController : ControllerBase
{
    private readonly IWithdrawalService _withdrawalService;
    private readonly ILogger<WithdrawalController> _logger;

    /// <summary>
    /// Initializes a new instance of the WithdrawalController.
    /// </summary>
    public WithdrawalController(IWithdrawalService withdrawalService, ILogger<WithdrawalController> logger)
    {
        _withdrawalService = withdrawalService;
        _logger = logger;
    }

    /// <summary>
    /// Processes an ATM withdrawal request and returns the optimal note distribution.
    /// </summary>
    /// <param name="request">The withdrawal request containing the amount.</param>
    /// <returns>A response containing the list of notes to dispense.</returns>
    /// <response code="200">Returns the list of notes for the withdrawal.</response>
    /// <response code="400">Invalid request (e.g., negative amount or cannot form with available notes).</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(WithdrawalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<WithdrawalResponse>> Withdraw([FromBody] WithdrawalRequest request)
    {
        _logger.LogInformation("Processing withdrawal request for amount: {Amount}", request.Amount);

        var result = await _withdrawalService.WithdrawAsync(request.Amount);

        var response = new WithdrawalResponse
        {
            Notes = result.Notes,
            TotalAmount = result.TotalAmount,
            NoteCount = result.NoteCount
        };

        _logger.LogInformation("Withdrawal successful. Dispensed {NoteCount} notes totaling {TotalAmount}",
            response.NoteCount, response.TotalAmount);

        return Ok(response);
    }

    /// <summary>
    /// Health check endpoint.
    /// </summary>
    /// <returns>Service status.</returns>
    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", service = "ATM Withdrawal API" });
    }
}
