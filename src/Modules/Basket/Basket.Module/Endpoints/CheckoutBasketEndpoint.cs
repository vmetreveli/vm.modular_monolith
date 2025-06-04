using Basket.Application.Features.Commands.CheckoutBasket;
using Basket.Module.Contracts;
using Carter;
using Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.Module.Endpoints;

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (
                CheckoutBasketRequest request,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await dispatcher.SendAsync(command,cancellationToken);

                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);
            })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket")
        .RequireAuthorization();
    }
}
