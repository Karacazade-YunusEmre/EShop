namespace EShop.Domain.Primitives;

/// <summary>
/// Kimliğe (Id) sahip, yaşam döngüsü boyunca kimliğiyle tanımlanan
/// varlıkların temel sınıfı. Eşitlik referansa göre değil, Id'ye göre belirlenir.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected init; } = default!;

    protected Entity(TId id)
    {
        Id = id;
    }

    // EF Core'un nesneyi veritabanından oluştururken kullanması için.
    // Uygulama kodunda bu ctor'u asla elle çağırmayız.
    protected Entity()
    {
    }


    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;

        // Farklı tipteki iki nesne, Id'leri aynı olsa bile eşit değildir.
        return GetType() == other.GetType() && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj) =>
        obj is Entity<TId> other && Equals(other);

    public override int GetHashCode() =>
        EqualityComparer<TId>.Default.GetHashCode(Id);

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) =>
        Equals(left, right);

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) =>
        !Equals(left, right);
}