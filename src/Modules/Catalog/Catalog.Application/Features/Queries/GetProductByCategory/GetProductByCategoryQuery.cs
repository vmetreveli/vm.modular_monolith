using Catalog.Application.Contracts;
using Meadow_Framework.Core.Abstractions.Queries;

namespace Catalog.Application.Features.Queries.GetProductByCategory;

public class GetProductByCategoryQuery : IQuery<GetProductByCategoryResult>
{
    public string Category { get; init; }
}