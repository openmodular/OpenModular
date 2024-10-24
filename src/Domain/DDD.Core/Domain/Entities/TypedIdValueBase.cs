﻿namespace OpenModular.DDD.Core.Domain.Entities;

public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Value { get; }

    protected TypedIdValueBase() : this(Guid.NewGuid())
    {
    }

    protected TypedIdValueBase(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        Value = value;
    }

    protected TypedIdValueBase(string value)
    {
        var val = Guid.Parse(value);

        if (val == Guid.Empty)
        {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        Value = val;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is TypedIdValueBase other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(TypedIdValueBase? other)
    {
        return Value == other?.Value;
    }

    public static bool operator ==(TypedIdValueBase? obj1, TypedIdValueBase? obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(TypedIdValueBase? x, TypedIdValueBase? y)
    {
        return !(x == y);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}