using Framework.Abstractions.Commands;
using Ordering.Application.Contracts;
using Ordering.Domain.Exceptions;
using Ordering.Infrastructure.Context;

namespace Ordering.Application.Features.Commands.DeleteOrder;

internal class DeleteOrderHandler(OrderingDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .FindAsync([command.OrderId], cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}
