using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;

/// <summary>
/// 认证命令
/// </summary>
public class AuthenticateCommand : CommandBase<AuthenticateDto>
{
    /// <summary>
    /// 租户编号
    /// </summary>
    public TenantId? TenantId { get; set; }

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
    /// 认证客户端
    /// </summary>
    public AuthenticationClient Client { get; set; }

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
