using System;
using System.Collections.Generic;

namespace Basket.Application.Contracts;
public class ShoppingCartDto
{
    public Guid Id { get; set; } 
    // public string UserName { get; set; } 
    public List<ShoppingCartItemDto> Items { get; set; } 
    
}
