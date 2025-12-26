using Basket.Application.Contracts;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Basket.Application.Features.Commands.DeleteBasket;

public class DeleteBasketCommand : ICommand<DeleteBasketResult>
{
    public string UserName { get; init; } 
}