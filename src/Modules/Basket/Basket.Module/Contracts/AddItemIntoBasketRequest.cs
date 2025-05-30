using Basket.Application.Contracts;

namespace Basket.Module.Contracts;

public record AddItemIntoBasketRequest(string UserName, ShoppingCartItemDto ShoppingCartItem);