using Framework.Abstractions.Queries;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Entities;
using Ordering.Domain.Exceptions;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;
using Ordering.Infrastructure.Specifications;

namespace Ordering.Application.Features.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository.FirstOrDefaultAsync(new OrdersWithItemSpecification(query.Id), cancellationToken);
  
        if (order is null)
        {
            throw new OrderNotFoundException(query.Id);
        }

        var orderDto = order.Adapt<OrderDto>();

        return new GetOrderByIdResult { Order = orderDto };

    }
}
