using Meadow_Framework.Core.Infrastructure.Repository;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;

namespace Ordering.Infrastructure.Repositories;


public class OrderUnitOfWork(OrderingDbContext dbContext)
    : UnitOfWork<OrderingDbContext>(dbContext), IOrderUnitOfWork;