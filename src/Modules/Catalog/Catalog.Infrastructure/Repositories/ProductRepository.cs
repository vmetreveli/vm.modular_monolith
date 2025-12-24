using Catalog.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Context;
using Meadow_Framework.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(CatalogDbContext dbContext) : RepositoryBase<CatalogDbContext, Product, Guid>(dbContext), IProductRepository
{
    private readonly DbContext _dbContext = dbContext;

    
    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

}