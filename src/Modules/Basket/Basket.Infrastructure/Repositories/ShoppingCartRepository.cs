using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.Domain.Entities;
using Basket.Domain.Exception;
using Basket.Domain.Repository;
using Basket.Infrastructure.Context;
using Framework.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infrastructure.Repositories;

public class ShoppingCartRepository(BasketDbContext dbContext) : RepositoryBase<BasketDbContext, ShoppingCart, Guid>(dbContext), IShoppingCartRepository

{
    private readonly DbContext _dbContext = dbContext;

    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<ShoppingCart>()
            .Include(x => x.Items)
            .Where(x => x.UserName == userName);

        if (asNoTracking) query.AsNoTracking();

        var basket = await query.SingleOrDefaultAsync(cancellationToken);

        return basket ?? throw new BasketNotFoundException(userName);
    }

    public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<ShoppingCart>().Add(basket);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await GetBasket(userName, false, cancellationToken);

        _dbContext.Set<ShoppingCart>().Remove(basket);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}