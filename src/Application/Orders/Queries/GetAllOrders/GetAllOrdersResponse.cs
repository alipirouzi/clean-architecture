using Domain.Entities;

namespace Application.Orders.Queries.GetAllOrders;

public sealed record GetAllOrdersResponse(int Count, List<Order> Orders);