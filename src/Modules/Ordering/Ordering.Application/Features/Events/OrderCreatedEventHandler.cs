﻿using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Features.Events;
// public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
// {
//     public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
//     {
//         logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
//         return Task.CompletedTask;
//     }
// }
