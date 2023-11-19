namespace Syrup.Result.Models;

/// <summary>
/// 
/// </summary>
public class ErrorResponse
{
    public string? ErrorMessage { get; set; }

    public string? ErrorParameters { get; set; }

    public ErrorResponse(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ErrorResponse(string? errorMessage, Exception? exception)
    {
        ErrorMessage = errorMessage;
    }
}
