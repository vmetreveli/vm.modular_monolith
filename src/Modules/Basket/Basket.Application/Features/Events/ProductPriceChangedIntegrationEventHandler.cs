using System.Threading.Tasks;
using Basket.Application.Features.Commands.UpdateItemPriceInBasket;
using Basket.Domain.Events;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Meadow_Framework.Core.Abstractions.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Features.Events;

public class ProductPriceChangedIntegrationEventHandler(IDispatcher dispatcher, ILogger<ProductPriceChangedIntegrationEventHandler> logger)
    : IEventConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        // mediatr new command and handler to find products on basket and update price
        var command = new UpdateItemPriceInBasketCommand(context.Message.ProductId, context.Message.Price);
        var result = await dispatcher.SendAsync(command);

        if (!result.IsSuccess)
            logger.LogError("Error updating price in basket for product id: {ProductId}", context.Message.ProductId);

        logger.LogInformation("Price for product id: {ProductId} updated in basket", context.Message.ProductId);
    }
}