using Carter;
using Discount.Application.Features.Discount.Commands.DeleteDiscount;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Discount.Module.Endpoints;

public sealed class DeleteDiscountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/discount/{productName}", async (
                string productName,
                IDispatcher dispatcher,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteDiscountCommand
                {
                    ProductName = productName
                };

                await dispatcher.SendAsync(command, cancellationToken);

                return Results.Ok();
            })
            .WithName("DeleteDiscount")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Discount")
            .WithDescription("Deletes discount by product name");
    }
}