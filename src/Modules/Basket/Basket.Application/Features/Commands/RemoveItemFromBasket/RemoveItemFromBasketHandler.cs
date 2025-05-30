using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Repository;
using Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.RemoveItemFromBasket;

internal class RemoveItemFromBasketHandler(IBasketRepository repository)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

        shoppingCart.RemoveItem(command.ProductId);

        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new RemoveItemFromBasketResult{Id = shoppingCart.Id};
    }
}
