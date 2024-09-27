namespace OpenModular.Common.Utils;

/// <summary>
/// 提供常用的正则表达式
/// </summary>
public sealed class RegexExpressionConstants
{
    /// <summary>
    /// 邮箱
    /// </summary>
    public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    /// <summary>
    /// IPv4
    /// </summary>
    public const string IPv4 = @"^((25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.){3}(25[0-5]|2[0-4]\\d|[01]?\\d\\d?)$";
}