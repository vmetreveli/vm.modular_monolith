using Meadow_Framework.Core.Abstractions.Events;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Ordering.Domain.Events;
using Ordering.Domain.Primitives;
using Ordering.Domain.Services;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Events;

public class OrderCreatedEventHandler(IEmailService emailService, ILogger<OrderCreatedEventHandler> logger)
    : IEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", @event.GetType().Name);
        await SendMail(@event.Order, cancellationToken);
    }


    private async Task SendMail(Order order, CancellationToken cancellationToken)
    {
        try
        {
            SendEmailDto emailDto = new()
            {
                To = Email.Create(order.ShippingAddress.EmailAddress),
                Subject = "Order was created",
                Body = $"Order {order.Id} is successfully created."
            };
            await emailService.SendEmailAsync(emailDto, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError("Order {OrderId} filed due to an error with mail service: {ExMessage}", order.Id,
                ex.Message);
        }
    }
}