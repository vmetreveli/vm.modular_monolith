using Basket.Infrastructure.Services.Catalog.Models;

namespace Basket.Infrastructure.Services.Catalog;

public interface IProductReadService
{
    public Task<Product> GetProductById(Guid id, CancellationToken cancellationToken);

}