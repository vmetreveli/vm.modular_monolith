using Basket.Application.Features.Commands.RemoveItemFromBasket;
using Basket.Module.Contracts;
using Carter;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Basket.Module.Endpoints;

//public record RemoveItemFromBasketRequest(string UserName, Guid ProductId);

public class RemoveItemFromBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}/items/{productId}",
            async ([FromRoute] string userName, 
                   [FromRoute] Guid productId, 
                   IDispatcher dispatcher,
                   CancellationToken cancellationToken) =>
            {
                var command = new RemoveItemFromBasketCommand{UserName = userName,ProductId = productId};

                var result = await dispatcher.SendAsync(command, cancellationToken);

                var response = result.Adapt<RemoveItemFromBasketResponse>();

                return Results.Ok(response);
            })
        .Produces<RemoveItemFromBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Remove Item From Basket")
        .WithDescription("Remove Item From Basket")
        .RequireAuthorization();
    }
}
