using Meadow_Framework.Core.Abstractions.Events;

namespace Basket.Domain.Events;

public class ShoppingCartItemAddedEvent : IDomainEvent
{
    public Guid ShoppingCartId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public string Color { get; init; } = default!;
    public decimal Price { get; init; } = default!;
    public string ProductName { get; init; } = default!;
}
