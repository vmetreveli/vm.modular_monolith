using Basket.Application.Contracts;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Basket.Application.Features.Commands.CreateBasket;

public class CreateBasketCommand : ICommand<CreateBasketResult>
{
    public ShoppingCartDto ShoppingCart { get; init; }

}