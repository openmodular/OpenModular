namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证模式
/// </summary>
public enum AuthenticationMode
{
    /// <summary>
    /// 密码认证
    /// </summary>
    Password,
    /// <summary>
    /// 单点登录
    /// </summary>
    SSO,
    /// <summary>
    /// 扫码登录
    /// </summary>
    QRCode,
    /// <summary>
    /// 应用内免登录
    /// </summary>
    InApp
}