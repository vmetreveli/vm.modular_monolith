using Carter;
using Meadow_Framework.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ordering.Application.Contracts;
using Ordering.Application.Contracts.Pagination;
using Ordering.Application.Features.Queries.GetOrders;

namespace Ordering.Module.Endpoints;

//public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrdersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var result = await dispatcher.QueryAsync(new GetOrdersQuery(request), cancellationToken);

            GetOrdersResponse response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}
