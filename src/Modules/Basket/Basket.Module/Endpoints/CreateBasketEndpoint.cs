using System.Security.Claims;
using Basket.Application.Contracts;
using Basket.Application.Features.Commands.CreateBasket;
using Basket.Module.Contracts;
using Carter;
using Meadow_Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.Module.Endpoints;

public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (
                CreateBasketRequest request,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                var userName = string.Empty;
             //   var updatedShoppingCart = new ShoppingCartDto { UserName = userName };

                var command = new CreateBasketCommand
                {
                    ShoppingCart = request.ShoppingCart
                };

                var result =  await dispatcher.SendAsync(command, cancellationToken);

                var response = result.Adapt<CreateBasketResponse>();

                return Results.Created($"/basket/{response.Id}", response);
            })
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket");
        //.RequireAuthorization();
    }
}