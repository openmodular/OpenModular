using System;
using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Users.Events;

/// <summary>
/// 用户激活领域事件
/// </summary>
/// <param name="UserId">用户标识</param>
/// <param name="ActivatedTime">激活时间</param>
public record UserActivatedDomainEvent(UserId UserId, DateTime ActivatedTime) : DomainEventBase;