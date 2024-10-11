using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

public class AccountDto : IDto
{
    public AccountId Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 账户状态
    /// </summary>
    public AccountStatus Status { get; set; }

    /// <summary>
    /// 已锁定
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// 激活时间
    /// </summary>
    public DateTimeOffset? ActivatedTime { get; set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public AccountId CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
}