using OpenModular.DDD.Domain.Events;

namespace OpenModular.DDD.Domain.Entities;

/// <inheritdoc/>
public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// 已发布的领域事件集合
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// 清除领域事件
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// 添加领域事件
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    protected Entity()
    {
        
    }

    protected Entity(TKey id)
    {
        Id = id;
    }

    public TKey? Id { get; set; }
}