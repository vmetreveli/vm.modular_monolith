using System;

namespace Basket.Application.Contracts;
public class ShoppingCartItemDto
{
    public Guid Id { get; set; }
    public Guid ShoppingCartId { get; set; } 
    public Guid ProductId { get; set; } 
    public int Quantity { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string ProductName { get; set; } 

}
