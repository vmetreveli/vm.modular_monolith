using System.Threading;
using System.Threading.Tasks;
using Basket.Domain.Events;
using Meadow_Framework.Core.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Features.Events;
public class ShoppingCartItemAddedEventHandler(ILogger<ShoppingCartItemAddedEventHandler> logger)
    : IDomainEventHandler<ShoppingCartItemAddedEvent>
{
    public Task HandleAsync(ShoppingCartItemAddedEvent @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
