using Catalog.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Framework.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(CatalogDbContext dbContext) : RepositoryBase<CatalogDbContext, Product, Guid>(dbContext), IProductRepository
{
    private readonly DbContext _dbContext = dbContext;

    
    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<(List<Product> Products, long TotalCount)> GetPaginatedProductsAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.Set<Product>().LongCountAsync(cancellationToken);

        var products = await _dbContext.Set<Product>()
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (products, totalCount);
    }

}