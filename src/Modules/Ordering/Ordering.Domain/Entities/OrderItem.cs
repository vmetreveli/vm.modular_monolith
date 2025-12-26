using Meadow_Framework.Core.Abstractions.Primitives;

namespace Ordering.Domain.Entities;
public class OrderItem : EntityBase<Guid>
{
    public OrderItem():base(Guid.NewGuid())
    {
        
    }
    public OrderItem(Guid orderId, Guid productId, int quantity, decimal price) : base(Guid.NewGuid())
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid OrderId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public int Quantity { get; internal set; } = default!;
    public decimal Price { get; private set; } = default!;
}
