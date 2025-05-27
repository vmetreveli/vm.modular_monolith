using Framework.Abstractions.Primitives;

namespace Basket.Domain.Entities;

public class ShoppingCart : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    private readonly List<ShoppingCartItem> _items = new();

    public ShoppingCart(Guid id) : base(id)
    {
    }

    private ShoppingCart() : base(Guid.NewGuid())
    {
        throw new NotImplementedException();
    }

    public string UserName { get; private set; } = default!;
    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }

    public static ShoppingCart Create(Guid id, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);

        var shoppingCart = new ShoppingCart
        {
            Id = id,
            UserName = userName
        };

        return shoppingCart;
    }

    public void AddItem(Guid productId, int quantity, string color, decimal price, string productName)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new ShoppingCartItem(Id, productId, quantity, color, price, productName);
            _items.Add(newItem);
        }
    }

    public void RemoveItem(Guid productId)
    {
        var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null) _items.Remove(existingItem);
    }
}