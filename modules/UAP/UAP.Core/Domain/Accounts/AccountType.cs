namespace OpenModular.Module.UAP.Core.Domain.Accounts;

/// <summary>
/// 账户类型
/// </summary>
public class AccountType
{
    private static readonly List<AccountType> _types = new();

    internal AccountType(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获取所有账户类型
    /// </summary>
    public static IEnumerable<AccountType> Types => _types;

    /// <summary>
    /// 获取或创建一个账户类型
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AccountType GetOrCreate(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var source = new AccountType(name);
        _types.Add(source);
        return source;
    }

    /// <summary>
    /// 获取指定账户类型
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AccountType? Find(string name)
    {
        return _types.FirstOrDefault(s => s.Name == name);
    }

    /// <summary>
    /// 本地账户
    /// </summary>
    public static AccountType Normal = GetOrCreate("Normal");

    // 重载 == 运算符
    public static bool operator ==(AccountType? left, AccountType? right)
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
    public static bool operator !=(AccountType? left, AccountType? right)
    {
        return !(left == right);
    }

    // 重写 Equals 方法
    public override bool Equals(object? obj)
    {
        if (obj is AccountType other)
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