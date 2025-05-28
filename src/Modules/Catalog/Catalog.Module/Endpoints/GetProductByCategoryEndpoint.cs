using Carter;
using Catalog.Application.Contracts;
using Catalog.Application.Features.Queries.GetProductByCategory;
using Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Module.Endpoints;

//public record GetProductByCategoryRequest(string Category);
public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.QueryAsync(new GetProductByCategoryQuery(category), cancellationToken);

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        //.WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
