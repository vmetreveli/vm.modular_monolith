using Meadow_Framework.Core.Abstractions.Commands;
using Ordering.Application.Contracts;

namespace Ordering.Application.Features.Commands.CreateOrder;

public class CreateOrderCommand : ICommand<CreateOrderResult>
{
    public OrderDto Order { get; init; }
}