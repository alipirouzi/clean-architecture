namespace SharedKernel;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public Guid Id { get; init; }

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