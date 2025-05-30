using Catalog.Application.Contracts;

namespace Catalog.Module.Endpoints;

public class CreateProductRequest
{
    public ProductDto Product { get; init; }
}