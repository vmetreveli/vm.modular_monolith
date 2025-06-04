using Catalog.Application.Contracts;
using Framework.Abstractions.Queries;

namespace Catalog.Application.Features.Queries.GetProductById;

public class GetProductByIdQuery: IQuery<GetProductByIdResult>
{
    public Guid Id { get; init; }
}