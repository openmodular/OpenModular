namespace OpenModular.Common.Utils.Extensions;

/// <summary>
/// GUID扩展方法
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    /// 是否为Guid.Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsEmpty(this Guid value)
    {
        return value == Guid.Empty;
    }

    /// <summary>
    /// 是否为Null或者Guid.Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this Guid? value)
    {
        return value == null || value == Guid.Empty;
    }

    /// <summary>
    /// 不为Guid.Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool NotEmpty(this Guid value)
    {
        return value != Guid.Empty;
    }

    /// <summary>
    /// 不为Null或者Guid.Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool NotNullAndEmpty(this Guid? value)
    {
        return value != null && value == Guid.Empty;
    }
}