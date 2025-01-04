using Application.Abstractions;

namespace Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(string Name, int Number) : ICommand;