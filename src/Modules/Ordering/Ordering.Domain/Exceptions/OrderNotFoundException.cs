using Meadow_Framework.Abstractions.Exceptions;

namespace Ordering.Domain.Exceptions;

public class OrderNotFoundException(Guid orderId) : InflowException("Order", orderId.ToString())
{
}
