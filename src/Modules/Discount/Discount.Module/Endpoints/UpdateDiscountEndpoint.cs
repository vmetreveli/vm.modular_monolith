using Carter;
using Discount.Application.Features.Discount.Commands.UpdateDiscount;
using Discount.Domain.Entities;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Discount.Module.Endpoints;

public sealed class UpdateDiscountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/discount", async (
                UpdateDiscountCommand command,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                var result = await dispatcher.SendAsync(command, cancellationToken);

                return Results.Ok(result);
            })
            .WithName("UpdateDiscount")
            .Produces(StatusCodes.Status200OK)
          //  .Produces<Coupon>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Discount")
            .WithDescription("Updates an existing discount");
    }

}