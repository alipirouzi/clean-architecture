using Domain.Abstractions;
using MediatR;

namespace Domain;

public record DomainEvent(Guid Id) : IDomainEvent, INotification;