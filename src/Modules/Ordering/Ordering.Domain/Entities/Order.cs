using Meadow_Framework.Core.Abstractions.Primitives;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;
public class Order : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    public Order(Guid id, DateTime createdOn, DateTime modifiedOn, bool isDeleted, DateTime? deletedOn, Guid customerId, string orderName, Address shippingAddress, Address billingAddress, Payment payment) : base(id)
    {
        CreatedOn = createdOn;
        ModifiedOn = modifiedOn;
        IsDeleted = isDeleted;
        DeletedOn = deletedOn;
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
    }

    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }
    
    private readonly List<OrderItem> _items = new();

    private Order() : base(Guid.NewGuid())
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public static Order Create(Guid id, Guid customerId, string orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment
        };

      //  order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Add(Guid productId, int quantity, decimal price)
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
            var orderItem = new OrderItem(Id, productId, quantity, price);
            _items.Add(orderItem);
        }
    }

    public void Remove(Guid productId)
    {
        var orderItem = _items.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem is not null)
        {
            _items.Remove(orderItem);
        }
    }


}
