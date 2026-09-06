namespace ApiDex.Domain.Results;

public sealed class Result<T> : Result
{
    private readonly T? _value;

    private Result(T value) : base(true, null)
    {
        _value = value;
    }

    private Result(Error error) : base(false, error)
    {
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Failure(error);
    }
}
