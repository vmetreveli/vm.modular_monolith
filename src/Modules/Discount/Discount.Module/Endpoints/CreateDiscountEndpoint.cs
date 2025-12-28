using Carter;
using Discount.Application.Features.Discount.Commands.CreateDiscount;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Discount.Module.Endpoints;

public sealed class CreateDiscountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/discount", async (
                CreateDiscountCommand command,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                await dispatcher.SendAsync(command, cancellationToken);

                return Results.CreatedAtRoute(
                    "GetDiscount",
                    new { productName = command.ProductName },
                    command);
            })
            .WithName("CreateDiscount")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Discount")
            .WithDescription("Creates a new discount");
    }
}