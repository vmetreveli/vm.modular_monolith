using Discount.Domain.Entities;
using Discount.Domain.Repository;
using Meadow_Framework.Core.Abstractions.Commands;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountCommandHandler(IDiscountRepository discountRepository)
    : ICommandHandler<CreateDiscountCommand>
{
    public async Task Handle(CreateDiscountCommand request, CancellationToken cancellationToken = default)
    {
        Coupon coupon = new()
        {
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount
        };


        await discountRepository.CreateDiscount(coupon, cancellationToken);
    }
}