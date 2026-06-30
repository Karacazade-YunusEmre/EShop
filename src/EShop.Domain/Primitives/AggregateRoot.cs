namespace EShop.Domain.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    /// <summary>
    /// Bu aggregate'te biriken, henüz yayınlanmamış domain olayları (yalnızca okunabilir).
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Aggregate'in kendi içinden bir domain olayı üretmesini sağlar.
    /// Yalnızca türeyen sınıf (örn. Order.Place içinde) çağırabilir.
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    /// <summary>
    /// Biriken olayları temizler. Olaylar yayınlandıktan sonra altyapı tarafından çağrılır.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}