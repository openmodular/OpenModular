namespace OpenModular.Module.UAP.API.Endpoints.Users.CreateUser;

/// <summary>
/// 创建用户请求
/// </summary>
public record CreateUserRequest
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
}