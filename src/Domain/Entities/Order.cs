using Domain.Abstractions;

namespace Domain.Entities;

public class Order : AggregateRoot
{
    private Order(Guid id, int orderNumber, string name, DateTime createdAtUtc)
        : base(id)
    {
        OrderNumber = orderNumber;
        Name = name;
        CreatedAtUtc = createdAtUtc;
    }

    public int OrderNumber { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }

    public static Order Create(Guid id, string name, int number, DateTime createdAtUtc)
    {
        var newOrder = new Order(id, number, name, createdAtUtc);
        newOrder.RaiseEvent(new OrderCreated(id));
        return newOrder;
    }

    public Order Update(Order order)
    {
        Name = order.Name;
        OrderNumber = order.OrderNumber;
        return this;
    }
}

public sealed record OrderCreated(Guid Id) : DomainEvent(Id);