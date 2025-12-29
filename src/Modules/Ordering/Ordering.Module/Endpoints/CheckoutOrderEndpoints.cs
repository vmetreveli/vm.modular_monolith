using Carter;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Ordering.Application.Contracts;
using Ordering.Application.Contracts.Pagination;
using Ordering.Application.Features.Queries.GetOrders;

namespace Ordering.Module.Endpoints;

public class CheckoutOrderEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/checkoutOrder",
                async ([AsParameters] PaginationRequest request, IDispatcher dispatcher,
                    CancellationToken cancellationToken) =>
                {
                    var result = await dispatcher.QueryAsync(new GetOrdersQuery(request), cancellationToken);

                    var response = result.Adapt<GetOrdersResponse>();

                    return Results.Ok(response);
                })
            .WithName("CheckoutOrder")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout Order")
            .WithDescription("Checkout Order");
    }
}