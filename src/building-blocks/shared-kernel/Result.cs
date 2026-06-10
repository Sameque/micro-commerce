namespace MicroCommerce.BuildingBlocks.SharedKernel;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

public class Result<TValue, TError>
{
    public bool IsSuccess { get; }
    public TValue? Value { get; }
    public TError? Error { get; }
    public bool IsFailure => !IsSuccess;

    private Result(TValue? value, bool isSuccess, TError? error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<TValue, TError> Success(TValue value) => new(value, true, default);
    public static Result<TValue, TError> Failure(TError error) => new(default, false, error);
}
