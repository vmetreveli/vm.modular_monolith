using Basket.Application.Contracts;

namespace Basket.Module.Contracts;

public class CreateBasketRequest()
{
    public ShoppingCartDto ShoppingCart { get; init; } 

}