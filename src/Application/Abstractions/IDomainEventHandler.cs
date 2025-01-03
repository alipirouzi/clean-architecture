using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions;

public interface IDomainEventHandler<in TEventType> : INotificationHandler<TEventType>
    where TEventType : IDomainEvent, INotification;