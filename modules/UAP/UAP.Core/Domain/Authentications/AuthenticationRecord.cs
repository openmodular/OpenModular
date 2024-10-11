using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Domain.Authentications;

/// <summary>
/// 认证记录
/// </summary>
public class AuthenticationRecord : AggregateRoot<int>
{
    /// <summary>
    /// 认证模式
    /// </summary>
    public AuthenticationMode Mode { get; }

    /// <summary>
    /// 认证源
    /// </summary>
    public AuthenticationSource Source { get; }

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

    /// <summary>
    /// 账户标识
    /// </summary>
    public AccountId AccountId { get; set; }

    /// <summary>
    /// 认证状态
    /// </summary>
    public AuthenticationStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 认证时间
    /// </summary>
    public DateTimeOffset AuthenticateTime { get; set; }

    public AuthenticationRecord()
    {
        //for ef
    }

    private AuthenticationRecord(AuthenticationMode mode, AuthenticationSource source, DateTimeOffset authenticateTime)
    {
        Mode = mode;
        Source = source;

        AuthenticateTime = authenticateTime;
    }

    /// <summary>
    /// 创建认证记录
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="source"></param>
    /// <param name="authenticateTime"></param>
    /// <returns></returns>
    public static AuthenticationRecord Create(AuthenticationMode mode, AuthenticationSource source, DateTimeOffset authenticateTime)
    {
        return new AuthenticationRecord(mode, source, authenticateTime);
    }
}