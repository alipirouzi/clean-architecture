using Domain.Entities;
using SharedKernel;

namespace Domain.Abstractions;

public interface IOrderRepository
{
    Task<Result<Order>> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Result<List<Order>>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
    Task<Result<Guid>> AddSingleOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<Result<List<Guid>>> AddManyOrdersAsync(List<Order> orders, CancellationToken cancellationToken = default);
    Task<Result> RemoveOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Result<Order>> UpdateOrderAsync(Guid orderId, Order order, CancellationToken cancellationToken = default);
}