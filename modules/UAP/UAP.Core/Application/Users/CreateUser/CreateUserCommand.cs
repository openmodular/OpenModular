using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Users.CreateUser;

/// <summary>
/// 创建用户命令
/// </summary>
public record CreateUserCommand : CommandBase<UserId>
{
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
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    public string ConfirmPassword { get; set; }
}