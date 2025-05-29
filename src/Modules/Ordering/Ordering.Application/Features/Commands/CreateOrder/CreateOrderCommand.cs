using Framework.Abstractions.Commands;
using Ordering.Application.Contracts;

namespace Ordering.Application.Features.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;