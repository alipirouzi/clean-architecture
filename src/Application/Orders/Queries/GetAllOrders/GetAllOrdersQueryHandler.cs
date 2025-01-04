using Application.Abstractions;
using Domain.Abstractions;
using SharedKernel;

namespace Application.Orders.Queries.GetAllOrders;

public sealed class GetAllOrdersQueryHandler(IOrderRepository orderRepo) : IQueryHandler<GetAllOrdersQuery, GetAllOrdersResponse>
{
    public async Task<Result<GetAllOrdersResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepo.GetAllOrdersAsync(cancellationToken);
        return Result.Success(new GetAllOrdersResponse(orders.Value.Count, orders.Value));
    }
}