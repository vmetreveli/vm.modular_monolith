namespace Catalog.Application.Contracts;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);