namespace Ordering.Application.Contracts;

public class OrderItemDto
{
    public OrderItemDto(Guid orderId, Guid productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public OrderItemDto()
    {
        
    }

    public Guid OrderId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }

    public void Deconstruct(out Guid orderId, out Guid productId, out int quantity, out decimal price)
    {
        orderId = OrderId;
        productId = ProductId;
        quantity = Quantity;
        price = Price;
    }
}