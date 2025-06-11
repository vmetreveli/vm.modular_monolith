using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Repository;
using Basket.Infrastructure.Services.Catalog;
using Framework.Abstractions.Commands;
using Framework.Abstractions.Dispatchers;

namespace Basket.Application.Features.Commands.AddItemIntoBasket;

internal class AddItemIntoBasketCommandHandler(
    IShoppingCartRepository repository, 
    IProductReadService productReadService,
    IDispatcher dispatcher) 
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken = default)
    {
        // Add shopping cart item into shopping cart
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

        //TODO: Before AddItem into SC, we should call Catalog Module GetProductById method
        // Get latest product information and set Price and ProductName when adding item into SC
        
        var result = await productReadService.GetProductById(command.ShoppingCartItem.ProductId, cancellationToken);

        shoppingCart.AddItem(
                command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Color,
                result.Price,
                result.Name);
                //command.ShoppingCartItem.Price,
                //command.ShoppingCartItem.ProductName);

        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemIntoBasketResult { Id = shoppingCart.Id };
    }
}
