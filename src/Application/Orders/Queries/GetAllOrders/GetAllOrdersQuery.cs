using Application.Abstractions;

namespace Application.Orders.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery : IQuery<GetAllOrdersResponse>;