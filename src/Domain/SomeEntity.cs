using SharedKernel;

namespace Domain;

public class SomeEntity : Entity
{
    internal SomeEntity(Guid id, string name, DateTime createdUtc) : base(id)
    {
        Name = name;
        CreatedUtc = createdUtc;
    }

    public string Name { get; init; }

    private DateTime CreatedUtc { get; init; }

    public static SomeEntity Create(Guid id, string name, DateTime createdUtc)
    {
        return new SomeEntity (id, name, createdUtc);
    }
}