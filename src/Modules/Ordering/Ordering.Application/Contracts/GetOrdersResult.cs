using Ordering.Application.Contracts.Pagination;

namespace Ordering.Application.Contracts;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);