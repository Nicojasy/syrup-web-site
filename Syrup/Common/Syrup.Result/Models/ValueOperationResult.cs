namespace Common.ResultLib.Models;
public class ValueOperationResult<T> : OperationResult
{
    public ValueOperationResult(T value) => Value = value;

    public ValueOperationResult(string errorMessage, Exception? exception)
        : base(errorMessage, exception)
    {
    }

    public T? Value { get; }
}
