using Basket.Application.Contracts;
using Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.CreateBasket;

public class CreateBasketCommand : ICommand<CreateBasketResult>
{
    public ShoppingCartDto ShoppingCart { get; init; }

}