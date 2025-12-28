using Discount.Domain.Entities;
using Discount.Domain.Repository;
using Meadow_Framework.Core.Abstractions.Queries;
using Meadow_Framework.Core.Infrastructure.Exceptions;

namespace Discount.Application.Features.Discount.Queries.GetDiscount;

public class GetDiscountQueryHandler(IDiscountRepository discountRepository) : IQueryHandler<GetDiscountQuery, CouponVm>
{

    public async Task<CouponVm> Handle(GetDiscountQuery request, CancellationToken cancellationToken = default)
    {
        Coupon? discount = await discountRepository.GetDiscount(request.ProductName, cancellationToken);

        if (discount is null) throw new ObjectNotFoundException("Discount not found.", "Discount.NotFound");

        CouponVm result = new ()
        {
            ProductName = discount.ProductName,
            Description = discount.Description,
            Amount = discount.Amount
        };
        return result;
    }
}