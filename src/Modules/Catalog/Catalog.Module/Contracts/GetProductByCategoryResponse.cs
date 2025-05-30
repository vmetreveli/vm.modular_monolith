using Catalog.Application.Contracts;

namespace Catalog.Module.Contracts;

public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);