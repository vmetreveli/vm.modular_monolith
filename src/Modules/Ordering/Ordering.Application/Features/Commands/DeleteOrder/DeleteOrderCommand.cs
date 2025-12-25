using Meadow_Framework.Abstractions.Commands;
using Ordering.Application.Contracts;

namespace Ordering.Application.Features.Commands.DeleteOrder;

public class DeleteOrderCommand : ICommand<DeleteOrderResult>
{
    public Guid OrderId { get; init; }
}