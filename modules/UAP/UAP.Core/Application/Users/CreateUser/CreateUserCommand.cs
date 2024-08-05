using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Users.CreateUser;

/// <summary>
/// 创建用户命令
/// </summary>
public class CreateUserCommand : CommandBase<UserId>
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

    /// <summary>
    /// 电话
    /// </summary>
    public string Tel { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 性别，0、未知；1、男；2、女
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public UserId CreatedBy { get; set; }
}