using Meadow_Framework.Abstractions.Queries;
using Ordering.Application.Contracts;

namespace Ordering.Application.Features.Queries.GetOrderById;

public class GetOrderByIdQuery : IQuery<GetOrderByIdResult>
{
    public Guid Id { get; init; } 
}