using Basket.Application.Contracts;
using Meadow_Framework.Abstractions.Queries;

namespace Basket.Application.Features.Queries.GetBasket;

public class GetBasketQuery: IQuery<GetBasketResult>
{
    public string UserName { get; init; } 
}