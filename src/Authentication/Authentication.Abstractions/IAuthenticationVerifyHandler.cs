namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证校验处理器
/// </summary>
public interface IAuthenticationVerifyHandler<TAccount> where TAccount : class
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task HandleAsync(AuthenticationContext<TAccount> context, CancellationToken cancellationToken);
}