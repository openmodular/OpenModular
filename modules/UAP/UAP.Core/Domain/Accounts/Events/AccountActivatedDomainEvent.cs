﻿using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Events;

/// <summary>
/// 账户激活领域事件
/// </summary>
/// <param name="Account">账户</param>
public record AccountActivatedDomainEvent(Account Account) : DomainEventBase;