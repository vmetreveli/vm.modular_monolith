using Meadow_Framework.Core.Abstractions.Commands;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommand : ICommand<bool>
{
    public string ProductName { get; set; }
}