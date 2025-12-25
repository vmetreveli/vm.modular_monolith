using Catalog.Domain.Entities;
using Meadow_Framework.Abstractions.Repository;

namespace Catalog.Domain.Repository;

public interface IProductRepository : IRepositoryBase<Product, Guid>
{
}