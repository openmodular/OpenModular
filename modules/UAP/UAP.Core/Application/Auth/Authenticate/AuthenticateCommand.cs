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
    /// 身份载体
    /// </summary>
    public string Payload { get; set; }

    /// <summary>
    /// 认证终端
    /// </summary>
    public AuthenticationTerminal Terminal { get; set; }

    /// <summary>
    /// IPv4地址
    /// </summary>
    public string IPv4 { get; set; }

    /// <summary>
    /// IPv6地址
    /// </summary>
    public string IPv6 { get; set; }

    /// <summary>
    /// MAC地址
    /// </summary>
    public string Mac { get; set; }
}
