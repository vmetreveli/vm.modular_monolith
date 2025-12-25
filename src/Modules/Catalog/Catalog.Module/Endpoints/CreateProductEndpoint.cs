using Carter;
using Catalog.Application.Features.Commands.CreateProduct;
using Meadow_Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Module.Endpoints;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, IDispatcher dispatcher, CancellationToken cancellation) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await dispatcher.SendAsync(command, cancellation);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
