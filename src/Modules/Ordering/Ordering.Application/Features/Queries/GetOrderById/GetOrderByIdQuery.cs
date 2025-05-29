using Framework.Abstractions.Queries;
using Ordering.Application.Contracts;

namespace Ordering.Application.Features.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id)
    : IQuery<GetOrderByIdResult>;