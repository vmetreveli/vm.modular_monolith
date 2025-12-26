using Meadow_Framework.Core.Abstractions.Queries;
using Ordering.Application.Contracts;
using Ordering.Application.Contracts.Pagination;

namespace Ordering.Application.Features.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;