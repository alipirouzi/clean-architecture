using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Orders.Commands;

public sealed class CreateOrderCommandHandler(
    ILogger<CreateOrderCommandHandler> logger,
    IDateTimeProvider dateTimeProvider,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand>
{
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newId = Guid.NewGuid();
        var newOrder = Order.Create(newId, request.Name, request.Number, dateTimeProvider.UtcNow);
        await orderRepository.AddSingleOrderAsync(newOrder, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}