// using Catalog.Application.Contracts;
// using Catalog.Application.Contracts.Pagination;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Routing;
//
// namespace Catalog.Application.Features.Queries.GetProducts;
//
// //public record GetProductsRequest(PaginationRequest PaginationRequest);
// public record GetProductsResponse(PaginatedResult<ProductDto> Products);
//
// public class GetProductsEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
//         {
//             var result = await sender.Send(new GetProductsQuery(request));
//
//             var response = result.Adapt<GetProductsResponse>();
//
//             return Results.Ok(response);
//         })
//         .WithName("GetProducts")
//         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
//         .ProducesProblem(StatusCodes.Status400BadRequest)
//         .WithSummary("Get Products")
//         .WithDescription("Get Products");
//     }
// }
