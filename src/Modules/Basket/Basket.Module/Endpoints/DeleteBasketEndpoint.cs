using Basket.Application.Features.Commands.DeleteBasket;
using Basket.Module.Contracts;
using Carter;
using Meadow_Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.Module.Endpoints;

//public record DeleteBasketRequest(string UserName);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (
                string userName, 
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.SendAsync(new DeleteBasketCommand{UserName = userName},cancellationToken);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket")
        .RequireAuthorization();
    }
}
