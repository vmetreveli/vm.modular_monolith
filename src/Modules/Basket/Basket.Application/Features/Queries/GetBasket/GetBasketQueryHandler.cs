using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Contracts;
using Basket.Domain.Repository;
using Meadow_Framework.Abstractions.Queries;
using Mapster;

namespace Basket.Application.Features.Queries.GetBasket;

internal class GetBasketQueryHandler(IShoppingCartRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken = default)
    {
        // get basket with userName
        var basket = await repository.GetBasket(query.UserName, true, cancellationToken);

        //mapping basket entity to shoppingcartdto
        var basketDto = basket.Adapt<ShoppingCartDto>();

        return new GetBasketResult{ShoppingCart=basketDto};
    }
}
