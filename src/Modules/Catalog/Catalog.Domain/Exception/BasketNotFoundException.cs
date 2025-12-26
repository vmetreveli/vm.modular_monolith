using Meadow_Framework.Core.Abstractions.Exceptions;

namespace Catalog.Domain.Exception;

public class BasketNotFoundException(string userName)
    : InflowException("ShoppingCart", userName)
{
}