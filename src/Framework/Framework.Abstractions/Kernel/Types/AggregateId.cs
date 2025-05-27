﻿namespace Framework.Abstractions.Kernel.Types;

public class AggregateId<T>(T value) : IEquatable<AggregateId<T>>
{
    public T Value { get; } = value;

    public bool Equals(AggregateId<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AggregateId<T>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value);
    }
}

public class AggregateId(Guid value) : AggregateId<Guid>(value)
{
    public AggregateId() : this(Guid.NewGuid())
    {
    }

    public static implicit operator Guid(AggregateId id)
    {
        return id.Value;
    }

    public static implicit operator AggregateId(Guid id)
    {
        return new AggregateId(id);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}