namespace Ordering.Application.Contracts;
public record OrderItemDto(Guid OrderId, Guid ProductId, int Quantity, decimal Price);
