using MediatR;

namespace OpenModular.DDD.Core.Domain.Events;

/// <summary>
/// 领域事件接口
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// 事件唯一编号
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// 事件发生时间
    /// </summary>
    DateTime OccurredOn { get; }
}