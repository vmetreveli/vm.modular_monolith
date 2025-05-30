using System;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Entities;
using Basket.Domain.Repository;
using Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.CreateBasket;

internal class CreateBasketHandler(IBasketRepository repository)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        //create Basket entity from command object
        //save to database
        //return result

        var shoppingCart = CreateNewBasket(command.ShoppingCart);        

        await repository.CreateBasket(shoppingCart, cancellationToken);

        return new CreateBasketResult(shoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
    {
        // create new basket
        var newBasket = ShoppingCart.Create(
            Guid.NewGuid(),
            shoppingCartDto.UserName);

        shoppingCartDto.Items.ForEach(item =>
        {
            newBasket.AddItem(
                item.ProductId,
                item.Quantity,
                item.Color,
                item.Price,
                item.ProductName);
        });

        return newBasket;
    }
}
