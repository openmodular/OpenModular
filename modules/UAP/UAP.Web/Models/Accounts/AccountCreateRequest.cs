namespace OpenModular.Module.UAP.Web.Models.Accounts;

/// <summary>
/// 用户创建请求
/// </summary>
public class AccountCreateRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    public required string UserName { get; set; }

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
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }
}