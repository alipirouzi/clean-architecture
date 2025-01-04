using Application.Abstractions;
using MediatR;

namespace Application.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : ICommand;