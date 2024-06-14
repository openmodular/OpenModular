
// ReSharper disable once CheckNamespace
namespace OpenModular;

public static class Check
{
    /// <summary>
    /// 检测对象是否为空
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName">参数名</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static T NotNull<T>(T value, string parameterName)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);

        return value;
    }

    /// <summary>
    /// 字符串不为 null, 空或者空白
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NotNullOrWhiteSpace(string value, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
        }

        return value;
    }
}