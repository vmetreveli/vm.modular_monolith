using System;
using System.Collections.Generic;

namespace Basket.Application.Contracts;
public class ShoppingCartDto
{
    public Guid Id { get; init; } 
    public string UserName { get; init; } 
    public List<ShoppingCartItemDto> Items { get; init; } 
    
}
