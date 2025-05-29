using Catalog.Domain.Entities;
using Framework.Abstractions.Repository;

namespace Catalog.Domain.Repository;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
}