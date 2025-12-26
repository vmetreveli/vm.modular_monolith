using System.Text.Json.Serialization;
using Meadow_Framework.Core.Abstractions.Primitives;

namespace Basket.Domain.Entities;

public class ShoppingCartItem : EntityBase<Guid>, IAuditableEntity, IDeletableEntity
{
    public ShoppingCartItem() : base(Guid.NewGuid())
    {
        
    }
    public ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity, string color, decimal price,
        string productName) : base(Guid.NewGuid())
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    [JsonConstructor]
    public ShoppingCartItem(Guid id, Guid shoppingCartId, Guid productId, int quantity, string color, decimal price,
        string productName) : base(id)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        Price = price;
        ProductName = productName;
    }

    public Guid ShoppingCartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; internal set; }
    public string Color { get; private set; } = default!;

    // will comes from Catalog module
    public decimal Price { get; private set; }
    public string ProductName { get; private set; } = default!;

    public void UpdatePrice(decimal newPrice)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newPrice);
        Price = newPrice;
    }

    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }
}