using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Events;

/// <summary>
/// 账户删除领域事件
/// </summary>
/// <param name="Account"></param>
public record AccountDeletedDomainEvent(Account Account) : DomainEventBase;