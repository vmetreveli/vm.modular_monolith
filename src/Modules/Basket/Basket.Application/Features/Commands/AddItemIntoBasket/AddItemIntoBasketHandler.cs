using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Repository;
using Catalog.Application.Features.Queries.GetProductById;
using Framework.Abstractions.Commands;
using Framework.Abstractions.Dispatchers;

namespace Basket.Application.Features.Commands.AddItemIntoBasket;

internal class AddItemIntoBasketHandler(IBasketRepository repository, IDispatcher dispatcher) 
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
    {
        // Add shopping cart item into shopping cart
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

        //TODO: Before AddItem into SC, we should call Catalog Module GetProductById method
        // Get latest product information and set Price and ProductName when adding item into SC
        
        var result = await dispatcher.QueryAsync(new GetProductByIdQuery(command.ShoppingCartItem.ProductId), cancellationToken);

        shoppingCart.AddItem(
                command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Color,
                result.Product.Price,
                result.Product.Name);
                //command.ShoppingCartItem.Price,
                //command.ShoppingCartItem.ProductName);

        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemIntoBasketResult(shoppingCart.Id);
    }
}
