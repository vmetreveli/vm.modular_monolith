using Basket.Domain.Entities;
using Framework.Abstractions.Repository;

namespace Basket.Domain.Repository;

public interface IShoppingCartRepository : IRepositoryBase<ShoppingCart, Guid>
{
    Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default);
}