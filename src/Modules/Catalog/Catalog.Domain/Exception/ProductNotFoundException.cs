using Meadow_Framework.Core.Abstractions.Exceptions;

namespace Catalog.Domain.Exception;

public class ProductNotFoundException(Guid id)
    : InflowException("Product", id.ToString())
{
}