using System;
using Basket.Application.Contracts;
using Meadow_Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.RemoveItemFromBasket;

public class RemoveItemFromBasketCommand : ICommand<RemoveItemFromBasketResult>
{
    public string UserName { get; init; } 
    public Guid ProductId { get; init; } 

}