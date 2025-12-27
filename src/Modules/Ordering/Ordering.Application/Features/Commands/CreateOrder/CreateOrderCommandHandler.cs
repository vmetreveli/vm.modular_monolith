using Meadow_Framework.Core.Abstractions.Commands;
using Meadow_Framework.Core.Abstractions.Repository;
using Ordering.Application.Contracts;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Commands.CreateOrder;

internal class CreateOrderCommandHandler(IOrderRepository orderRepository,IOrderUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var order = CreateNewOrder(command.Order);

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new CreateOrderResult(order.Id);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var newOrder = Order.Create(
                id: Guid.NewGuid(),
                customerId: orderDto.CustomerId,
                orderName: $"{orderDto.OrderName}_{new Random().Next()}",
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                );

        orderDto.Items.ForEach(item =>
        {
            newOrder.Add(
                item.ProductId,
                item.Quantity,
                item.Price);
        });

        return newOrder;
    }
}
