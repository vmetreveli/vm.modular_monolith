using Basket.Application.Contracts;
using Meadow_Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.AddItemIntoBasket;

public class AddItemIntoBasketCommand : ICommand<AddItemIntoBasketResult>
{
    public string UserName { get; init; } 
    public ShoppingCartItemDto ShoppingCartItem { get; init; }

}