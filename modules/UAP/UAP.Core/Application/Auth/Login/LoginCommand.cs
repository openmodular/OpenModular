using OpenModular.DDD.Core.Application.Command;

namespace OpenModular.Module.UAP.Core.Application.Auth.Login;

/// <summary>
/// 登录命令
/// </summary>
public class LoginCommand : CommandBase<LoginDto>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string Captcha { get; set; }
}