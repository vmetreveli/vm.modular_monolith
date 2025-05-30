using Catalog.Domain.Events;
using Framework.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Events;
public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) : IDomainEventHandler<ProductCreatedEvent>
{

    public Task HandleAsync(ProductCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
