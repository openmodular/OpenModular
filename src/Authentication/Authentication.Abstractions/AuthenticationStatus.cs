namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证状态
/// </summary>
public enum AuthenticationStatus
{
    /// <summary>
    /// 成功
    /// </summary>
    Success,

    /// <summary>
    /// 无效的认证身份
    /// </summary>
    InvalidIdentity,

    /// <summary>
    /// 无效的图形验证码
    /// </summary>
    InvalidImageCaptcha,

    /// <summary>
    /// 用户不存在
    /// </summary>
    UserNotFound,

    /// <summary>
    /// 密码错误
    /// </summary>
    IncorrectPassword,

    /// <summary>
    /// 用户被锁定
    /// </summary>
    UserLocked,

    /// <summary>
    /// 用户被禁用
    /// </summary>
    UserDisabled,

    /// <summary>
    /// 认证模式无效
    /// </summary>
    InvalidMode,

    /// <summary>
    /// 认证源无效
    /// </summary>
    InvalidSource,

    /// <summary>
    /// 验证码错误
    /// </summary>
    IncorrectCaptcha,

    /// <summary>
    /// 认证超时
    /// </summary>
    Timeout,
    /// <summary>
    /// 认证环节找不到
    /// </summary>
    VerifyStageNotFound
}