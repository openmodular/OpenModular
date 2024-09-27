namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证校验阶段处理器
/// </summary>
public interface IAuthenticationVerifyStageHandler<TUser> where TUser : class
{
    /// <summary>
    /// 阶段唯一名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 校验处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task HandleAsync(AuthenticationContext<TUser> context, CancellationToken cancellationToken);
}