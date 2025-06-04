using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Throw;

namespace Basket.Infrastructure;

public static class ThrowExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Validatable<TValue> ThrowIfNullOrEmpty<TValue>(
        [NotNull] [AllowNull] this TValue? value,
        ExceptionCustomizations? exceptionCustomizations = null,
        [CallerArgumentExpression("value")] string? paramName = null)
        where TValue : notnull, IEnumerable
    {
        return value.ThrowIfNull(exceptionCustomizations, paramName).IfEmpty();
    }
}