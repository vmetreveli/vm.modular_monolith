using Catalog.Domain.Entities;
using Framework.Abstractions.Repository;

namespace Catalog.Domain.Repository;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
    public Task<(List<Product> Products, long TotalCount)> GetPaginatedProductsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
}