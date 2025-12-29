using Discount.Domain.Entities;
using Discount.Domain.Repository;
using Meadow_Framework.Core.Abstractions.Commands;


namespace Discount.Application.Features.Discount.Commands.UpdateDiscount;

public class UpdateProductCommandHandler(IDiscountRepository discountRepository)
    : ICommandHandler<UpdateDiscountCommand, bool>
{
    public async Task<bool> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken = default)
    {
        Coupon coupon = new()
        {
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount
        };
        return await discountRepository.UpdateDiscount(coupon, cancellationToken);
    }
}