using Catalog.Application.Contracts;

namespace Catalog.Application.Features.Queries.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);