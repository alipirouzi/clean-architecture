using Application.Abstractions;

namespace Application.Orders.Queries.GetSingleOrder;

public sealed record GetSingleOrderQuery(Guid OrderId) : IQuery<GetSingleOrderResponse>;