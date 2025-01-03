using Application.Abstractions;

namespace Application.Orders.Commands;

public sealed record CreateOrderCommand(string Name, int Number) : ICommand;