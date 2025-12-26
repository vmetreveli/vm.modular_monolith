using Meadow_Framework.Core.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderingDbContext dbContext) : RepositoryBase<OrderingDbContext, Order, Guid>(dbContext), IOrderRepository;