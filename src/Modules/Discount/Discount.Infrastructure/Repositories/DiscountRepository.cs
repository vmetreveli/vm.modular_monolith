using Discount.Domain.Entities;
using Discount.Domain.Repository;
using Discount.Infrastructure.Context;
using Meadow_Framework.Core.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories;

public class DiscountRepository(DiscountDbContext dbContext) :  RepositoryBase<DiscountDbContext, Coupon , Guid>(dbContext), IDiscountRepository
{
    public Task<Coupon> GetDiscount(string productName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateDiscount(Coupon coupon, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDiscount(Coupon coupon, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDiscount(string productName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}