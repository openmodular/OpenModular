namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证身份处理器
/// </summary>
public interface IAuthenticationIdentityHandler<TAccount> where TAccount : class
{
    /// <summary>
    /// 认证模式
    /// </summary>
    AuthenticationMode Mode { get; }

    /// <summary>
    /// 认证源
    /// </summary>
    AuthenticationSource Source { get; }

    /// <summary>
    /// 解析身份信息
    /// </summary>
    /// <param name="payload">身份载体(JSON格式字符串)</param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task HandleAsync(string? payload, AuthenticationContext<TAccount> context, CancellationToken cancellationToken);
}