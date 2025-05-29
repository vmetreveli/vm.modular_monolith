using Framework.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repository;
using Ordering.Infrastructure.Context;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderingDbContext dbContext) : RepositoryBase<OrderingDbContext, Order, Guid>(dbContext), IOrderRepository
{
    private readonly DbContext _dbContext = dbContext;

    
    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

 }