using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Events;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreated>
{
    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("Order created with {Id}", notification.Id);
        return Task.CompletedTask;
    }
}