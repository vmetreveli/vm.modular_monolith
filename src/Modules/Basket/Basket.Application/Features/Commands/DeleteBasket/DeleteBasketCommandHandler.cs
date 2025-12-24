using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Repository;
using Meadow_Framework.Abstractions.Commands;

namespace Basket.Application.Features.Commands.DeleteBasket;

internal class DeleteBasketCommandHandler(IShoppingCartRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken = default)
    {
        //Delete Basket entity from command object
        //save to database
        //return result
      
        await repository.DeleteBasket(command.UserName, cancellationToken);

        return new DeleteBasketResult(true);
    }
}
