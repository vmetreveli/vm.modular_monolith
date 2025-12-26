using Meadow_Framework.Core.Abstractions.Exceptions;

namespace Basket.Domain.Exception;

public class BasketNotFoundException(string userName)
    : InflowException("ShoppingCart", userName)
{
}