using System.Net;
using System.Text.Json;
using ATMWithdrawal.API.Models;
using ATMWithdrawal.Domain.Exceptions;

namespace ATMWithdrawal.API.Middleware;

/// <summary>
/// Global exception handling middleware that catches and transforms exceptions
/// into standardized error responses.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles exceptions by mapping them to appropriate HTTP status codes and error responses.
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occurred while processing the request");

        var errorResponse = exception switch
        {
            InvalidArgumentException => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorType = "InvalidArgument",
                Message = exception.Message
            },
            NoteUnavailableException => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorType = "NoteUnavailable",
                Message = exception.Message
            },
            _ => new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorType = "InternalServerError",
                Message = "An unexpected error occurred. Please try again later."
            }
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
