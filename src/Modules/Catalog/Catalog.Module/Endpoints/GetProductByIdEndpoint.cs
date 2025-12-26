using Carter;
using Catalog.Application.Features.Queries.GetProductById;
using Catalog.Module.Contracts;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Module.Endpoints;

//public record GetProductByIdRequest(Guid Id);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, IDispatcher dispatcher,CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.QueryAsync(new GetProductByIdQuery{Id = id},cancellationToken);

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
