using System;
using System.Data;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Entities;
using Basket.Domain.Events;
using Basket.Domain.Exception;
using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Basket.Infrastructure.Specifications;
using Meadow_Framework.Core.Abstractions.Commands;
using Meadow_Framework.Core.Abstractions.Outbox;
using Meadow_Framework.Core.Abstractions.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Basket.Application.Features.Commands.CheckoutBasket;

internal class CheckoutBasketCommandHandler(IUnitOfWork unitOfWork, IOutboxRepository outboxRepository,IShoppingCartRepository shoppingCartRepository)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken = default)
    {
        // get existing basket with total price
        // Set totalprice on basketcheckout event message
        // send basket checkout event to rabbitmq using masstransit
        // delete the basket

      await unitOfWork.BeginTransactionAsync( IsolationLevel.ReadCommitted,null,cancellationToken: cancellationToken);

        try
        {
            // Get existing basket with total price
            ShoppingCart? basket = await shoppingCartRepository.FirstOrDefaultAsync(new ShoppingCartWithItemSpecification(command.BasketCheckout.UserName),cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(command.BasketCheckout.UserName);
            }

            // Set total price on basket checkout event message
            var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutIntegrationEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            // Write a message to the outbox
            var outboxMessage = new OutboxMessage(JsonSerializer.Serialize(eventMessage),Guid.NewGuid(), DateTime.UtcNow);
         

            outboxRepository.CreateOutboxMessage(outboxMessage);

            // Delete the basket
            shoppingCartRepository.Remove(basket);

            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new CheckoutBasketResult(true);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return new CheckoutBasketResult(false);
        }

        ///////////////////// CHECKOUT BASKET WITHOUT OUTBOX
        //var basket =
        //    await repository.GetBasket(command.BasketCheckout.UserName, true, cancellationToken);

        //var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutIntegrationEvent>();
        //eventMessage.TotalPrice = basket.TotalPrice;

        //await bus.Publish(eventMessage, cancellationToken);

        //await repository.DeleteBasket(command.BasketCheckout.UserName, cancellationToken);

        //return new CheckoutBasketResult(true);
        ///////////////////// CHECKOUT BASKET WITHOUT OUTBOX
    }
}
