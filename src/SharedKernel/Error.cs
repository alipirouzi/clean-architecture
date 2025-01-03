namespace SharedKernel;

public sealed record  Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullType = new("Error.NullValue", "Null value provided.");
    //public static implicit operator Result(Error error) => Result.Failure(error);
}