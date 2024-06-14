namespace OpenModular.DDD.Domain.Events;

/// <summary>
/// 领域事件基类
/// </summary>
public abstract class DomainEventBase : IDomainEvent
{
    /// <inheritdoc/>
    public Guid Id { get; } = Guid.NewGuid();

    /// <inheritdoc/>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}