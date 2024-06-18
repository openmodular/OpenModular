using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Users.Events;

/// <summary>
/// 用户创建领域事件
/// </summary>
/// <param name="User">用户</param>
public record UserCreatedDomainEvent(User User) : DomainEventBase;