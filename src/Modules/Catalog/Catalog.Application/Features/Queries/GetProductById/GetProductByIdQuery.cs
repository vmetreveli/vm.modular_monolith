using Catalog.Application.Contracts;
using Framework.Abstractions.Queries;

namespace Catalog.Application.Features.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(ProductDto Product);
