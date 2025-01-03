using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Persistence.Repositories;

public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    public async Task<Result<Order>> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
        if (order != null) return Result.Success(order);
        var error = new Error("Error.NullOrder", "Null order provided.");
        return error;
    }

    public async Task<Result<List<Order>>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);
        return Result.Success(orders);
    }

    public async Task<Result<Guid>> AddSingleOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await dbContext.Orders.AddAsync(order, cancellationToken);
        return Result.Success(order.Id);
    }

    public async Task<Result<List<Guid>>> AddManyOrdersAsync(List<Order> orders,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Orders.AddRangeAsync(orders, cancellationToken);
        return Result.Success(orders.Select(o => o.Id).ToList());
    }

    public async Task<Result> RemoveOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        if (order is null) return new Error("Error.NotFound", $"Order with id {orderId} not found.");
        dbContext.Orders.Remove(order);
        return Result.Success();
    }

    public async Task<Result<Order>> UpdateOrderAsync(Guid orderId, Order order,
        CancellationToken cancellationToken = default)
    {
        var oldOrder = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        if (oldOrder is null) return new Error("Error.NotFound", $"Order with id {orderId} not found.");
        var newOrder = order.Update(order);
        return Result.Success(newOrder);
    }
}