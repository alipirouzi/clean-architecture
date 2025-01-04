using Application.Abstractions;
using Domain.Abstractions;
using SharedKernel;

namespace Application.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteOrderCommand>
{
    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var deleteResult = await orderRepository.RemoveOrderByIdAsync(request.OrderId, cancellationToken);
        if (deleteResult.IsSuccessful)
            await unitOfWork.SaveChangesAsync(cancellationToken);
        return deleteResult;
    }
}