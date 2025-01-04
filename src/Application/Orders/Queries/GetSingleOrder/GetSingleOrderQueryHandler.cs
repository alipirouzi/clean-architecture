using Application.Abstractions;
using Domain.Abstractions;
using SharedKernel;

namespace Application.Orders.Queries.GetSingleOrder;

public sealed class GetSingleOrderQueryHandler(IOrderRepository orderRepository) :
    IQueryHandler<GetSingleOrderQuery, GetSingleOrderResponse>
{
    public async Task<Result<GetSingleOrderResponse>> Handle(GetSingleOrderQuery request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);
        return order.IsSuccessful
            ? Result.Success(new GetSingleOrderResponse(order.Value.Id, order.Value.OrderNumber, order.Value.Name,
                order.Value.CreatedAtUtc))
            : order.Error;
    }
}