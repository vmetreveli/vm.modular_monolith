using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Entities;
using Basket.Domain.Exception;
using Basket.Domain.Repository;
using Basket.Infrastructure.Specifications;
using Mapster;
using Meadow_Framework.Core.Abstractions.Commands;
using Meadow_Framework.Core.Abstractions.Events;
using Meadow_Framework.Core.Abstractions.Repository;
using vm.modular.IntegrationEvents;

namespace Basket.Application.Features.Commands.CheckoutBasket;

internal class CheckoutBasketCommandHandler(
    IBasketUnitOfWork unitOfWork,
    IShoppingCartRepository shoppingCartRepository,
    IEventDispatcher eventDispatcher)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken = default)
    {
        await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, null, cancellationToken: cancellationToken);

        try
        {
            // Get existing basket with total price
            ShoppingCart? basket = await shoppingCartRepository.FirstOrDefaultAsync(new ShoppingCartWithItemSpecification(command.BasketCheckout.UserName), cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(command.BasketCheckout.UserName);
            }

            // Set total price on basket checkout event message
            var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutIntegrationEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            // Delete the basket
            shoppingCartRepository.Remove(basket);

            await unitOfWork.CompleteAsync(cancellationToken);

            // Publish before committing: a broker failure rolls the whole checkout back,
            // so the basket survives and the checkout can be retried.
            await eventDispatcher.PublishIntegrationEventAsync(eventMessage, cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new CheckoutBasketResult(true);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return new CheckoutBasketResult(false);
        }
    }
}
