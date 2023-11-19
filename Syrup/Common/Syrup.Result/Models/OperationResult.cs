namespace Common.ResultLib.Models;
public class OperationResult
{
    public OperationResult() => IsSuccess = true;

    public OperationResult(string errorMessage, Exception? exception)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public bool IsSuccess { get; }

    public string? ErrorMessage { get; }

    public Exception? Exception { get; }
}
