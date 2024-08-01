using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Users.Events;

/// <summary>
/// 用户删除领域事件
/// </summary>
/// <param name="User"></param>
public record UserDeletedDomainEvent(User User) : DomainEventBase;