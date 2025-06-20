﻿using Framework.Abstractions.Dispatchers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts;
using Ordering.Application.Features.Commands.CreateOrder;
using Ordering.Domain.Events;

namespace Ordering.Application.Features.Events;
public class BasketCheckoutIntegrationEventHandler(IDispatcher dispatcher, ILogger<BasketCheckoutIntegrationEventHandler> logger)
    : IConsumer<BasketCheckoutIntegrationEvent>
{
    public async  Task Consume(ConsumeContext<BasketCheckoutIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        // Create new order and start order fullfillment process
        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await dispatcher.SendAsync(createOrderCommand);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutIntegrationEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine,
            message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.Cvv,
            message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            orderId,
            message.CustomerId,
            message.UserName,
            addressDto,
            addressDto,
            paymentDto,
            [
                new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
            ]);

        return new CreateOrderCommand{ Order = orderDto};
    }


}
