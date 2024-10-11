namespace OpenModular.Module.UAP.Web.Models.Accounts;

/// <summary>
/// 用户创建请求
/// </summary>
public class AccountCreateRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    public required string ConfirmPassword { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public required string Phone { get; set; }

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
}