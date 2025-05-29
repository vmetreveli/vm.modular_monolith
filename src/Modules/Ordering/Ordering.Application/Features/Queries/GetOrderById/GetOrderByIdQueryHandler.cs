using Framework.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Exceptions;
using Ordering.Infrastructure.Context;

namespace Ordering.Application.Features.Queries.GetOrderById;

internal class GetOrderByIdQueryHandler(OrderingDbContext dbContext)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(query.Id);
        }

        //var orderDto = order.Adapt<OrderDto>();

       // return new GetOrderByIdResult(orderDto);
       throw new NotImplementedException();
    }
}
