using Carter;
using Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ordering.Application.Features.Commands.CreateOrder;
using Ordering.Module.Contracts;

namespace Ordering.Module.Endpoints;

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateOrderCommand>();

            var result = await dispatcher.SendAsync(command, cancellationToken);

            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/Orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}
