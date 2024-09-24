namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证终端
/// </summary>
public class AuthenticationTerminal
{
    private static readonly List<AuthenticationTerminal> _clients = new();

    internal AuthenticationTerminal(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 终端名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获取所有认证终端
    /// </summary>
    public static IEnumerable<AuthenticationTerminal> Clients => _clients;

    /// <summary>
    /// 创建一个认证终端
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AuthenticationTerminal GetOrCreate(string name)
    {
        var source = new AuthenticationTerminal(name);
        _clients.Add(source);
        return source;
    }

    /// <summary>
    /// 获取指定认证终端
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AuthenticationTerminal Find(string name)
    {
        return _clients.FirstOrDefault(s => s.Name == name);
    }

    /// <summary>
    /// Web端
    /// </summary>
    public static AuthenticationTerminal Web = GetOrCreate(nameof(Web));

    // 重载 == 运算符
    public static bool operator ==(AuthenticationTerminal left, AuthenticationTerminal right)
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
    public static bool operator !=(AuthenticationTerminal left, AuthenticationTerminal right)
    {
        return !(left == right);
    }

    // 重写 Equals 方法
    public override bool Equals(object obj)
    {
        if (obj is AuthenticationTerminal other)
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