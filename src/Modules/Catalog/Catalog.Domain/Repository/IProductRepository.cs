using Catalog.Domain.Entities;
using Meadow_Framework.Core.Abstractions.Repository;

namespace Catalog.Domain.Repository;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
    public Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default);
}