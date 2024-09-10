using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Application.Command;

namespace OpenModular.Module.UAP.Core.Application.Auth.Authenticate;

/// <summary>
/// 认证命令
/// </summary>
public class AuthenticateCommand : CommandBase<AuthenticateDto>
{
    /// <summary>
    /// 认证模式
    /// </summary>
    public AuthenticationMode Mode { get; set; }

    /// <summary>
    /// 认证源
    /// </summary>
    public AuthenticationSource Source { get; set; }

    /// <summary>
    /// 身份信息
    /// </summary>
    public string IdentityJson { get; set; }
}