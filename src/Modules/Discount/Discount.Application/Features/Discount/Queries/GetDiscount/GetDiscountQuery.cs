using Meadow_Framework.Core.Abstractions.Queries;

namespace Discount.Application.Features.Discount.Queries.GetDiscount;

public class GetDiscountQuery : IQuery<CouponVm>
{
    public string ProductName { get; set; }
}