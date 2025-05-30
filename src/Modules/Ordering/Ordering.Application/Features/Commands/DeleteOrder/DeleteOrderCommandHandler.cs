using Framework.Abstractions.Commands;
using Framework.Abstractions.Repository;
using Ordering.Application.Contracts;
using Ordering.Domain.Exceptions;
using Ordering.Domain.Repository;

namespace Ordering.Application.Features.Commands.DeleteOrder;

internal class DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        orderRepository.Remove(order);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return new DeleteOrderResult(true);
    }
}
