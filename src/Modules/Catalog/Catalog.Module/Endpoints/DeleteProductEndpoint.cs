using Carter;
using Catalog.Application.Features.Commands.DeleteProduct;
using Catalog.Module.Contracts;
using Meadow_Framework.Core.Abstractions.Dispatchers;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Module.Endpoints;

public class DeleteProductEndpoint : ICarterModule
 {
     public void AddRoutes(IEndpointRouteBuilder app)
     {
         app.MapDelete("/products/{id}", async (Guid id, IDispatcher dispatcher,CancellationToken cancellationToken) =>
         {
             var result = await dispatcher.SendAsync(new DeleteProductCommand(id),cancellationToken);

             var response = result.Adapt<DeleteProductResponse>();

             return Results.Ok(response);
         })
         .WithName("DeleteProduct")
         .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Delete Product")
         .WithDescription("Delete Product");
     }
 }
