using Catalog.Application.Contracts;

namespace Catalog.Module.Contracts;

public class CreateProductRequest
{
    public ProductDto Product { get; init; }
}