namespace Syrup.Result.Models;
public class ErrorResponse
{
    public string? ErrorMessage { get; set; }

    public string? ErrorParameters { get; set; }

    //todo: add type for response

    public ErrorResponse(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ErrorResponse(string? errorMessage, Exception? exception)
    {
        ErrorMessage = errorMessage;
    }
}
