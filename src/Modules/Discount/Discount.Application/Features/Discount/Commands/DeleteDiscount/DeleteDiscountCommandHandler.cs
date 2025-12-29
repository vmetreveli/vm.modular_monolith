using Discount.Domain.Repository;
using Meadow_Framework.Core.Abstractions.Commands;


namespace Discount.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    : ICommandHandler<DeleteDiscountCommand, bool>
{
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken = default)
    {
        return await discountRepository.DeleteDiscount(request.ProductName, cancellationToken);
    }
}