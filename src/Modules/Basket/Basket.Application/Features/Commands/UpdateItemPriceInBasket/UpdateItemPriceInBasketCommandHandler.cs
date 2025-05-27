using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.Infrastructure.Context;
using Framework.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;

namespace Basket.Application.Features.Commands.UpdateItemPriceInBasket;

internal class UpdateItemPriceInBasketCommandHandler(BasketDbContext dbContext)
    : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
{
    public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command,
        CancellationToken cancellationToken)
    {
        //Find Shopping Cart Items with a give ProductId
        //Iterate items and Update Price of every item with incoming command.Price
        //save to database
        //return result

        var itemsToUpdate = await dbContext.ShoppingCartItems
            .Where(x => x.ProductId == command.ProductId)
            .ToListAsync(cancellationToken);

        if (!itemsToUpdate.Any()) return new UpdateItemPriceInBasketResult(false);

        foreach (var item in itemsToUpdate) item.UpdatePrice(command.Price);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateItemPriceInBasketResult(true);
    }
}