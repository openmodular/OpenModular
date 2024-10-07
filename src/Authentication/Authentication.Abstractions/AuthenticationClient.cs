namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证客户端
/// </summary>
public class AuthenticationClient
{
    private static readonly List<AuthenticationClient> _clients = new();

    internal AuthenticationClient(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 客户端名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获取所有认证客户端
    /// </summary>
    public static IEnumerable<AuthenticationClient> Clients => _clients;

    /// <summary>
    /// 创建一个认证客户端
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AuthenticationClient GetOrCreate(string name)
    {
        var source = new AuthenticationClient(name);
        _clients.Add(source);
        return source;
    }

    /// <summary>
    /// 获取指定认证客户端
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AuthenticationClient Find(string name)
    {
        return _clients.FirstOrDefault(s => s.Name == name);
    }

    /// <summary>
    /// Web端
    /// </summary>
    public static AuthenticationClient Web = GetOrCreate(nameof(Web));

    // 重载 == 运算符
    public static bool operator ==(AuthenticationClient left, AuthenticationClient right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Name == right.Name;
    }

    // 重载 != 运算符
    public static bool operator !=(AuthenticationClient left, AuthenticationClient right)
    {
        return !(left == right);
    }

    // 重写 Equals 方法
    public override bool Equals(object obj)
    {
        if (obj is AuthenticationClient other)
        {
            return this == other;
        }

        return false;
    }

    // 重写 GetHashCode 方法
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}