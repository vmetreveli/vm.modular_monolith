using Carter;
using Discount.Application.Features.Discount.Queries.GetDiscount;
using Discount.Module.Contracts;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Discount.Module.Endpoints;

public sealed class GetDiscountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getDiscount", async (string productName, IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            GetDiscountQuery query = new()
            {
                ProductName = productName
            };

            CouponVm result = await dispatcher.QueryAsync(query, cancellationToken);

            return Results.Ok(result);
        })
        .WithName("GetDiscount")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Discount")
        .WithDescription("Get Discount");
    }
}
