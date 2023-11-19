using Common.ResultLib.Models;

namespace Common.ResultLib.Helpers;

/// <summary>
/// Helper for working with classes based on <seealso cref="OperationResult"/> 
/// </summary>
public static class ResultHelper
{
    public static OperationResult Success() =>
        new();

    public static ValueOperationResult<T> Success<T>(T value) =>
        new(value);

    public static OperationResult Bad(string errorMessage) =>
        new(errorMessage, null);

    public static ValueOperationResult<T> Bad<T>(string errorMessage) =>
        new(errorMessage, null);

    public static OperationResult Bad(string errorMessage, Exception exception) =>
        new(errorMessage, exception);

    public static ValueOperationResult<T> Bad<T>(string errorMessage, Exception exception) =>
        new(errorMessage, exception);

    public static ErrorResponse? GetErrorResponse(this OperationResult result) =>
        new(result.ErrorMessage, result.Exception);
}
