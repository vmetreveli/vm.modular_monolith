using MapsterMapper;
using Meadow_Framework.Core.Abstractions.Commands;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;

namespace Ordering.Application.Features.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler(
    IOrderRepository orderRepository,
    ILogger<CheckoutOrderCommandHandler> logger)
    : ICommandHandler<CheckoutOrderCommand, string>
{
    public async Task<string> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        // Order? orderEntity = Order.Create(
        //     Guid.NewGuid(),
        //     request.UserId,
        //     request.OrderItems,
        //     request.TotalAmount,
        //     request.ShippingAddress,
        //     request.EmailAddress);
        // // orderEntity = new()
        // // {
        // //     UserId = request.UserId,
        // //     OrderItems = request.OrderItems,
        // //     TotalAmount = request.TotalAmount,
        // //     ShippingAddress = request.ShippingAddress,
        // //     EmailAddress = request.EmailAddress
        // // };
        // await orderRepository.AddAsync(orderEntity, cancellationToken);
        // logger.LogInformation("Order {Id} is successfully created.", orderEntity.Id);
        // return orderEntity.Id.ToString();
    }
}