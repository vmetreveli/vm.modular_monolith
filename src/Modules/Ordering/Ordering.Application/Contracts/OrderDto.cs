namespace Ordering.Application.Contracts;
public class OrderDto
{
    public OrderDto(
        Guid id,
        Guid customerId, 
        string orderName,
        AddressDto shippingAddress,
        AddressDto billingAddress,
        PaymentDto payment,
        List<OrderItemDto> items)
    {
        Id = id;
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Items = items;
    }

    public OrderDto()
    {
        
    }

    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string OrderName { get; init; } 
    public AddressDto ShippingAddress { get; init; }
    public AddressDto BillingAddress { get; init; }
    public PaymentDto Payment { get; init; }
    public List<OrderItemDto> Items { get; init; }

    public void Deconstruct(out Guid id, out Guid customerId, out string orderName, out AddressDto shippingAddress, out AddressDto billingAddress, out PaymentDto payment, out List<OrderItemDto> items)
    {
        id = Id;
        customerId = CustomerId;
        orderName = OrderName;
        shippingAddress = ShippingAddress;
        billingAddress = BillingAddress;
        payment = Payment;
        items = Items;
    }
}