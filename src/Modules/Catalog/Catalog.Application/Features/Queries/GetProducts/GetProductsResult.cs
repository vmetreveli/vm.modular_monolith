using Catalog.Application.Contracts;
using Catalog.Application.Contracts.Pagination;

namespace Catalog.Application.Features.Queries.GetProducts;

public record GetProductsResult(PaginatedResult<ProductDto> Products);