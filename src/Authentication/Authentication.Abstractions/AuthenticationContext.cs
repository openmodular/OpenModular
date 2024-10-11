namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证上下文
/// </summary>
public class AuthenticationContext<TAccount> where TAccount : class
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
    /// 认证成功
    /// </summary>
    public bool Success => Status == AuthenticationStatus.Success;

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
    /// 认证时间
    /// </summary>
    public DateTimeOffset AuthenticateTime { get; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 账户信息
    /// </summary>
    public TAccount Account { get; set; }

    /// <summary>
    /// 认证状态
    /// </summary>
    public AuthenticationStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; set; }
}