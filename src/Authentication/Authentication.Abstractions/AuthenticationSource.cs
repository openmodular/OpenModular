namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证源
/// </summary>
public sealed class AuthenticationSource
{
    private static readonly List<AuthenticationSource> _sources = new();

    internal AuthenticationSource(string schema)
    {
        Schema = schema;
    }

    /// <summary>
    /// 认证源架构
    /// </summary>
    public string Schema { get; }

    /// <summary>
    /// 获取所有认证源列表
    /// </summary>
    public static IEnumerable<AuthenticationSource> Sources => _sources;

    /// <summary>
    /// 创建一个认证源
    /// </summary>
    /// <param name="schema"></param>
    /// <returns></returns>
    public static AuthenticationSource Create(string schema)
    {
        var source = new AuthenticationSource(schema);
        _sources.Add(source);
        return source;
    }

    /// <summary>
    /// 本地账户
    /// </summary>
    public static AuthenticationSource Local = Create("Local");

    /// <summary>
    /// AD账户
    /// </summary>
    public static AuthenticationSource ActiveDirectory = Create("ActiveDirectory");

    /// <summary>
    /// 企业微信
    /// </summary>
    public static AuthenticationSource WeCom = Create("WeCom");

    /// <summary>
    /// 钉钉
    /// </summary>
    public static AuthenticationSource DingTalk = Create("DingTalk");

    /// <summary>
    /// 飞书
    /// </summary>
    public static AuthenticationSource FeiShu = Create("FeiShu");
}