using Catalog.Application.Contracts.Pagination;
using Meadow_Framework.Abstractions.Queries;

namespace Catalog.Application.Features.Queries.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetProductsResult>;