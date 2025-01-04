namespace Application.Orders.Queries.GetSingleOrder;

public sealed record GetSingleOrderResponse(Guid Id, int OrderNumber, string Name, DateTime CreatedAtUtc);