using System.Data.Common;

namespace SharedKernel;

public abstract class Entity : IEquatable<Entity>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public Guid Id { get; private init; }

    public bool Equals(Entity? other)
    {
        if (other == null)
            return false;
        if (other.GetType() != GetType())
            return false;
        return other.Id == Id;
    }

    public static bool operator ==(Entity? left, Entity? right) =>
        left is not null
        && right is not null
        && left.Equals(right);

    public static bool operator !=(Entity? left, Entity? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != GetType())
            return false;
        if (obj is not Entity entity)
            return false;
        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 13;
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    private Entity()
    {
    }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}