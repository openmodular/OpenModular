using OpenModular.Authentication.Abstractions;
using OpenModular.Authentication.JwtBearer;

namespace OpenModular.Module.UAP.Web.Models.Authentications;

public class LoginResponse
{
    /// <summary>
    /// 认证状态
    /// </summary>
    public AuthenticationStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 令牌
    /// </summary>
    public JwtSecurityToken? Jwt { get; set; }
}