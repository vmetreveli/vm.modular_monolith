using Basket.Application.Contracts;
using Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.DeleteBasket;

public class DeleteBasketCommand : ICommand<DeleteBasketResult>
{
    public string UserName { get; init; } 
}