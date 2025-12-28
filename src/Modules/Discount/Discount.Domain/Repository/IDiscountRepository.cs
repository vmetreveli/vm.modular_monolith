using Discount.Domain.Entities;
using Meadow_Framework.Core.Abstractions.Repository;

namespace Discount.Domain.Repository;

public interface IDiscountRepository : IRepositoryBase<Coupon, Guid>
{
    Task<Coupon> GetDiscount(string productName, CancellationToken cancellationToken);

    Task<bool> CreateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> UpdateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> DeleteDiscount(string productName, CancellationToken cancellationToken);
}