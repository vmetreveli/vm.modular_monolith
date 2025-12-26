using Catalog.Domain.Events;
using Meadow_Framework.Core.Abstractions.Events;
using Meadow_Framework.Core.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Features.Events;
public class ProductPriceChangedEventHandler(IEventDispatcher eventDispatcher, ILogger<ProductPriceChangedEventHandler> logger) : IDomainEventHandler<ProductPriceChangedEvent>
{

    public async Task HandleAsync(ProductPriceChangedEvent @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", @event.GetType().Name);        
        
        // Publish product price changed integration event for update basket prices
        var integrationEvent = new ProductPriceChangedIntegrationEvent
        {
            ProductId = @event.Product.Id,
            Name = @event.Product.Name,
            Category = @event.Product.Category,
            Description = @event.Product.Description,
            ImageFile = @event.Product.ImageFile,
            Price = @event.Product.Price //set updated product price
        };
        
        await eventDispatcher.PublishIntegrationEventAsync(integrationEvent, cancellationToken);
    }
}
