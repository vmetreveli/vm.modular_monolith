using Framework.Abstractions.Queries;
using Ordering.Application.Contracts;
using Ordering.Infrastructure.Context;

namespace Ordering.Application.Features.Queries.GetOrders;

internal class GetOrdersQueryHandler(OrderingDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var pageIndex = query.PaginationRequest.PageIndex;
        // var pageSize = query.PaginationRequest.PageSize;
        //
        // var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        //
        // var orders = await dbContext.Orders
        //                 .AsNoTracking()
        //                 .Include(x => x.Items)
        //                 .OrderBy(p => p.OrderName)
        //                 .Skip(pageSize * pageIndex)
        //                 .Take(pageSize)
        //                 .ToListAsync(cancellationToken);
        //
        // var orderDtos = orders.Adapt<List<OrderDto>>();
        //
        // return new GetOrdersResult(
        //     new PaginatedResult<OrderDto>(
        //         pageIndex,
        //         pageSize,
        //         totalCount,
        //         orderDtos));
    }
}
