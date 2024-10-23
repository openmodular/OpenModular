using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 当前账户
/// </summary>
public interface ICurrentAccount
{
    /// <summary>
    /// 租户标识
    /// </summary>
    TenantId? TenantId { get; }

    /// <summary>
    /// 账户标识
    /// </summary>
    AccountId? AccountId { get; }

    /// <summary>
    /// 角色列表
    /// </summary>
    List<string> Roles { get; }
}