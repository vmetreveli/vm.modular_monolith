using Basket.Application.Features.Queries.GetBasket;
using Carter;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using GetBasketResponse = Basket.Module.Contracts.GetBasketResponse;

namespace Basket.Module.Endpoints;

//public record GetBasketRequest(string UserName); 

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (
                string userName,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                var result = await dispatcher.QueryAsync(new GetBasketQuery { UserName = userName }, cancellationToken);

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket");
        //.RequireAuthorization();
    }
}
