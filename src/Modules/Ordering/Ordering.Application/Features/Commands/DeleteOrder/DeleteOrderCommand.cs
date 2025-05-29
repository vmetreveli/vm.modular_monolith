using Framework.Abstractions.Commands;
using Ordering.Application.Contracts;

namespace Ordering.Application.Features.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId)
    : ICommand<DeleteOrderResult>;