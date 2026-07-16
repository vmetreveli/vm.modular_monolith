using System.Threading;
using System.Threading.Tasks;
using Basket.Domain.Events;
using Meadow_Framework.Core.Abstractions.Kernel;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Features.Events;
public class ShoppingCartItemRemovedEventHandler(ILogger<ShoppingCartItemRemovedEventHandler> logger)
    : IDomainEventHandler<ShoppingCartItemRemovedEvent>
{
    public Task HandleAsync(ShoppingCartItemRemovedEvent @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
