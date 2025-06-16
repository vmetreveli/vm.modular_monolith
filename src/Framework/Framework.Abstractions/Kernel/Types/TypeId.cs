namespace Framework.Abstractions.Kernel.Types;

public abstract class TypeId : IEqualityComparer<TypeId>
{
    protected TypeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }


    public bool IsEmpty()
    {
        return Value == Guid.Empty;
    }


    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static implicit operator Guid(TypeId typeId)
    {
        return typeId.Value;
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }

    public bool Equals(TypeId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((TypeId)obj);
    }

    public bool Equals(TypeId? x, TypeId? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;
        return x.Value.Equals(y.Value);
    }

    public int GetHashCode(TypeId obj)
    {
        return obj.Value.GetHashCode();
    }
}