using OpenModular.DDD.Core.Domain.Events;

namespace OpenModular.Module.UAP.Core.Domain.Users.Events;

/// <summary>
/// 用户创建领域事件
/// </summary>
/// <param name="UserId">用户标识</param>
/// <param name="CreatedBy">创建人标识</param>
/// <param name="CreatedAt">创建时间</param>
public record UserCreatedDomainEven(UserId UserId, UserId CreatedBy, DateTime CreatedAt) : DomainEventBase;