﻿using OpenModular.DDD.Core.Domain.Events;
using OpenModular.DDD.Core.Domain.Exceptions;

namespace OpenModular.DDD.Core.Domain.Entities;

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

    /// <summary>
    /// 检测业务规则
    /// </summary>
    /// <param name="rule"></param>
    /// <exception cref="BusinessRuleValidationException"></exception>
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
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

    public TKey? Id { get; set; }
}