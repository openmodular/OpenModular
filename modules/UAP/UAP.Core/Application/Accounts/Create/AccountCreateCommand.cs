using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Create;

/// <summary>
/// 创建账户命令
/// </summary>
public class AccountCreateCommand : Command<AccountId>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    public string? ConfirmPassword { get; set; }

    /// <summary>
    /// 用户状态
    /// </summary>
    public AccountStatus Status { get; set; }
}