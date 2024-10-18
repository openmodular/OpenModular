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
    /// 手机号
    /// </summary>
    public const string Mobile = @"^(?:(?:\+|00)86)?1(?:(?:3[\d])|(?:4[5-79])|(?:5[0-35-9])|(?:6[5-7])|(?:7[0-8])|(?:8[\d])|(?:9[01256789]))\d{8}$";

    /// <summary>
    /// IPv4
    /// </summary>
    public const string IPv4 = @"^((25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.){3}(25[0-5]|2[0-4]\\d|[01]?\\d\\d?)$";
}