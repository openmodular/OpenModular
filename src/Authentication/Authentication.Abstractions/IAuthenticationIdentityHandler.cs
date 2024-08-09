namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证身份处理器
/// </summary>
public interface IAuthenticationIdentityHandler<TUser> where TUser : class
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
    /// 处理
    /// </summary>
    /// <param name="identityJson">身份标识JSON格式字符串</param>
    /// <param name="context"></param>
    /// <returns></returns>
    Task HandleAsync(string identityJson, IAuthenticationContext<TUser> context);
}