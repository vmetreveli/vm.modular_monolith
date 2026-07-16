using Meadow_Framework.Core.Abstractions.Events;

namespace Basket.Domain.Events;

public class ShoppingCartItemRemovedEvent : IDomainEvent
{
    public Guid ShoppingCartId { get; init; }
    public Guid ProductId { get; init; }
}
