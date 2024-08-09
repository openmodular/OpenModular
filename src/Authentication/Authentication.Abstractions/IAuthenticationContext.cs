namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证上下文
/// </summary>
public interface IAuthenticationContext<TUser> where TUser : class
{
    /// <summary>
    /// 认证模式
    /// </summary>
    AuthenticationMode Mode { get; set; }

    /// <summary>
    /// 认证源
    /// </summary>
    string Source { get; set; }

    /// <summary>
    /// 认证源编号
    /// </summary>
    string SourceId { get; set; }

    /// <summary>
    /// 认证成功
    /// </summary>
    bool Success { get; set; }

    /// <summary>
    /// 用户信息
    /// </summary>
    TUser User { get; set; }

    /// <summary>
    /// 认证状态
    /// </summary>
    AuthenticationStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    string Message { get; set; }
}