using MapsterMapper;
using Meadow_Framework.Core.Abstractions.Commands;
using Meadow_Framework.Core.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;

namespace Ordering.Application.Features.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderUnitOfWork orderUnitOfWork,
    ILogger<UpdateOrderCommandHandler> logger)
    : ICommandHandler<UpdateOrderCommand>
{
    private readonly ILogger<UpdateOrderCommandHandler> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));


    private readonly IOrderRepository _orderRepository =
        orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

    private readonly IOrderUnitOfWork _orderUnitOfWork =
        orderUnitOfWork ?? throw new ArgumentNullException(nameof(orderUnitOfWork));

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken = default)
    {
        Order? orderToUpdate = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (orderToUpdate == null) throw new ObjectNotFoundException(nameof(Order), request.Id.ToString());

        //
        // orderToUpdate.ShippingAddress = request.ShippingAddress;
        // orderToUpdate.OrderItems = request.OrderItems;
        // orderToUpdate.TotalAmount = request.TotalAmount;
        //


        await _orderUnitOfWork.CompleteAsync(cancellationToken);

        _logger.LogInformation("Order {Guid} is successfully updated.", orderToUpdate.Id);
    }
}