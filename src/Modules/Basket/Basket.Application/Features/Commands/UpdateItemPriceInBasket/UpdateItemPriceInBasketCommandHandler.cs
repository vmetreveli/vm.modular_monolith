using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Basket.Infrastructure.Specifications;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Basket.Application.Features.Commands.UpdateItemPriceInBasket;

internal class UpdateItemPriceInBasketCommandHandler(IShoppingCartRepository shoppingCartRepository, BasketDbContext dbContext)
    : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
{
    public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command,
        CancellationToken cancellationToken = default)
    {
        //Find Shopping Cart Items with a give ProductId
        //Iterate items and Update Price of every item with incoming command.Price
        //save to database
        //return result

        // var itemsToUpdate = await dbContext.ShoppingCartItems
        //     .Where(x => x.ProductId == command.ProductId)
        var specification = new ShoppingCartWithItemSpecification(command.ProductId);
       var shoppingCarts = await shoppingCartRepository.FindAsync(specification, cancellationToken);

       var itemsToUpdate = shoppingCarts.FirstOrDefault()?.Items;
        

        if (!itemsToUpdate.Any()) return new UpdateItemPriceInBasketResult{IsSuccess = false};

        foreach (var item in itemsToUpdate) item.UpdatePrice(command.Price);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateItemPriceInBasketResult{IsSuccess = true};
    }
}