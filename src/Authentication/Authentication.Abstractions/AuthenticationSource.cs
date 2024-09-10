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
    public static AuthenticationSource Find(string schema)
    {
        return _sources.FirstOrDefault(s => s.Schema == schema);
    }

    /// <summary>
    /// 本地账户
    /// </summary>
    public static AuthenticationSource Local = GetOrCreate("Local");
}