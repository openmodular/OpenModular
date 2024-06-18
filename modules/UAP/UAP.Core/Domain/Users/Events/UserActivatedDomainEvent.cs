using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Users.Events;

/// <summary>
/// 用户激活领域事件
/// </summary>
/// <param name="User">用户</param>
public record UserActivatedDomainEvent(User User) : DomainEventBase;