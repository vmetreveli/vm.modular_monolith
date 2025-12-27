using Meadow_Framework.Core.Abstractions.Repository;
using Meadow_Framework.Core.Infrastructure.Repository;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;

namespace Ordering.Infrastructure.Repositories;

public class OrderUnitOfWork(OrderingDbContext context) : UnitOfWork<OrderingDbContext>(context), IOrderUnitOfWork;