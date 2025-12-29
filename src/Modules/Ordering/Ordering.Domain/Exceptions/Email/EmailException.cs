using Meadow_Framework.Core.Abstractions.Exceptions;

namespace Ordering.Domain.Exceptions.Email;

// public class NullOrEmptyException() : InflowException("FirstName.NullOrEmpty", "The first name is required.");
public class EmailException(string code, string message)
    : InflowException(code, message);