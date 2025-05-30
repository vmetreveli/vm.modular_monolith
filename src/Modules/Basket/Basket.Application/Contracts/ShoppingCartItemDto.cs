using System;

namespace Basket.Application.Contracts;
public class ShoppingCartItemDto
{
    public Guid Id { get; init; }
    public Guid ShoppingCartId { get; init; } 
    public Guid ProductId { get; init; } 
    public int Quantity { get; init; }
    public string Color { get; init; }
    public decimal Price { get; init; }
    public string ProductName { get; init; } 

}
