using OpenModular.DDD.Core.Domain.Events;
using OpenModular.Module.Abstractions.Exceptions;

namespace OpenModular.DDD.Core.Domain.Entities;

/// <inheritdoc/>
public abstract class Entity : IEntity
{
    /// <summary>
    /// 发布领域事件
    /// </summary>
    /// <param name="domainEvent"></param>
    public void PublishDomainEvent(IDomainEvent domainEvent)
    {
        DomainEventManager.Instance.Add(domainEvent);
    }

    /// <summary>
    /// 检测业务规则
    /// </summary>
    /// <param name="rule"></param>
    /// <exception cref="ModuleBusinessException"></exception>
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new ModuleBusinessException(rule.ModuleCode, rule.ErrorCode);
        }
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

    public TKey Id { get; private set; }
}