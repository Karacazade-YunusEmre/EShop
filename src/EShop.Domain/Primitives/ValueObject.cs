namespace EShop.Domain.Primitives;

/// <summary>
/// Kimliği olmayan, taşıdığı değerlerle tanımlanan nesnelerin temel sınıfı.
/// Eşitlik, GetEqualityComponents ile bildirilen alanların sırasına göre belirlenir.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return other is not null &&
               GetType() == other.GetType() &&
               GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj) =>
        obj is ValueObject other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var component in GetEqualityComponents())
        {
            hash.Add(component);
        }

        return hash.ToHashCode();
    }

    public static bool operator ==(ValueObject? left, ValueObject? right) =>
        Equals(left, right);

    public static bool operator !=(ValueObject? left, ValueObject? right) =>
        !Equals(left, right);
}