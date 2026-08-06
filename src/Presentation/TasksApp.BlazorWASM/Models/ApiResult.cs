namespace TasksApp.BlazorWASM.Models;

public class ApiResult
{
    public bool IsSuccess { get; protected set; }
    public CustomProblemDetails? Error { get; protected set; }

    public static ApiResult Success() => new() { IsSuccess = true };

    public static ApiResult Failure(CustomProblemDetails error) => new()
    {
        IsSuccess = false,
        Error = error
    };
}

public sealed class ApiResult<T> : ApiResult
{
    public T? Data { get; private set; }

    public static ApiResult<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };

    public new static ApiResult<T> Failure(CustomProblemDetails error) => new()
    {
        IsSuccess = false,
        Error = error
    };
}
