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
    public static AuthenticationSource GetOrCreate(string schema)
    {
        var source = new AuthenticationSource(schema);
        _sources.Add(source);
        return source;
    }

    /// <summary>
    /// 获取指定认证源
    /// </summary>
    /// <param name="schema"></param>
    /// <returns></returns>
    public static AuthenticationSource? Find(string schema)
    {
        return _sources.FirstOrDefault(s => s.Schema == schema);
    }

    /// <summary>
    /// 本地账户
    /// </summary>
    public static AuthenticationSource Local = GetOrCreate("Local");

    /// <summary>
    /// AD账户
    /// </summary>
    public static AuthenticationSource ActiveDirectory = GetOrCreate("ActiveDirectory");

    /// <summary>
    /// 微信
    /// </summary>
    public static AuthenticationSource WeChat = GetOrCreate("WeChat");

    /// <summary>
    /// 企业微信
    /// </summary>
    public static AuthenticationSource WeCom = GetOrCreate("WeCom");

    /// <summary>
    /// 钉钉
    /// </summary>
    public static AuthenticationSource DingTalk = GetOrCreate("DingTalk");

    /// <summary>
    /// 飞书
    /// </summary>
    public static AuthenticationSource FeiShu = GetOrCreate("FeiShu");

    // 重载 == 运算符
    public static bool operator ==(AuthenticationSource? left, AuthenticationSource? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Schema == right.Schema;
    }

    // 重载 != 运算符
    public static bool operator !=(AuthenticationSource? left, AuthenticationSource? right)
    {
        return !(left == right);
    }

    // 重写 Equals 方法
    public override bool Equals(object? obj)
    {
        if (obj is AuthenticationSource other)
        {
            return this == other;
        }

        return false;
    }

    // 重写 GetHashCode 方法
    public override int GetHashCode()
    {
        return Schema.GetHashCode();
    }
}