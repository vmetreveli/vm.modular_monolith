using Meadow_Framework.Core.Abstractions.Commands;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountCommand : ICommand
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}