namespace Catalog.Application.Contracts;

public class GetProductByCategoryResult
{
    public IEnumerable<ProductDto> Products { get; init; }
}