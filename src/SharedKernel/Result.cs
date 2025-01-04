namespace SharedKernel;

public class Result
{
    protected Result(bool isSuccessful, Error error)
    {
        if ((isSuccessful && error != Error.None) || (!isSuccessful && error == Error.None))
            throw new ArgumentException("Invalid error", nameof(error));
        IsSuccessful = isSuccessful;
        Error = error;
    }

    public bool IsSuccessful { get; }
    public bool IsFailure => !IsSuccessful;
    public Error Error { get; }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccessful, Error error)
        : base(isSuccessful, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccessful
        ? _value!
        : throw new InvalidOperationException("Value of a failure result cant be accessed.");

    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullType);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return Failure<TValue>(error);
    }
}