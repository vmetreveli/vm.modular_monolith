using Meadow_Framework.Core.Abstractions.Specifications;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Specifications;

/// <summary>
///     Represents the specification for determining the user with email.
/// </summary>
public sealed class OrdersWithItemSpecification : Specification<Order, Guid>
{
    public OrdersWithItemSpecification(Guid orderId) : base(order => order.Id == orderId) =>
        AddInclude(order => order.Items);

}