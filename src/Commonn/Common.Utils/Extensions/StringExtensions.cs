using System.Text;

// ReSharper disable once CheckNamespace
namespace System;

/// <summary>
/// 字符串扩展方法
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 转换成Byte
    /// </summary>
    /// <param name="s">输入字符串</param>
    /// <returns></returns>
    public static byte ToByte(this string s)
    {
        if (s.IsNullOrWhiteSpace())
            return 0;

        byte.TryParse(s, out byte result);
        return result;
    }

    /// <summary>
    /// 转换成Char
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static char ToChar(this string s)
    {
        if (s.IsNullOrWhiteSpace())
            return default;

        char.TryParse(s, out char result);
        return result;
    }

    /// <summary>
    /// 转换成short/Int16
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static short ToShort(this string s)
    {
        if (s.IsNullOrWhiteSpace())
            return 0;

        short.TryParse(s, out short result);
        return result;
    }

    /// <summary>
    /// 判断字符串是否为Null、空，等同于string.IsNullOrWhiteSpace
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this string s)
    {
        return string.IsNullOrWhiteSpace(s);
    }

    /// <summary>
    /// 判断字符串是否不为Null、空，等同于string.IsNullOrWhiteSpace
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsNotNullOrWhiteSpace(this string s)
    {
        return !string.IsNullOrWhiteSpace(s);
    }

    /// <summary>
    /// 与字符串进行比较，忽略大小写
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool EqualsIgnoreCase(this string s, string value)
    {
        return s.Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 匹配字符串结尾，忽略大小写
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool EndsWithIgnoreCase(this string s, string value)
    {
        return s.EndsWith(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 匹配字符串开头，忽略大小写
    /// </summary>
    /// <param name="s"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool StartsWithIgnoreCase(this string s, string value)
    {
        return s.StartsWith(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 首字母转小写
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string FirstCharToLower(this string s)
    {
        string str = s.First().ToString().ToLower() + s.Substring(1);
        return str;
    }

    /// <summary>
    /// 首字母转大写
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string FirstCharToUpper(this string s)
    {
        string str = s.First().ToString().ToUpper() + s.Substring(1);
        return str;
    }

    /// <summary>
    /// 转为Base64，UTF-8格式
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToBase64(this string s)
    {
        return s.ToBase64(Encoding.UTF8);
    }

    /// <summary>
    /// 转为Base64
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encoding">编码</param>
    /// <returns></returns>
    public static string ToBase64(this string s, Encoding encoding)
    {
        var bytes = encoding.GetBytes(s);
        return bytes.ToBase64();
    }

    /// <summary>
    /// 转换为Base64
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToBase64(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Base64解析
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string FromBase64(this string s)
    {
        byte[] data = Convert.FromBase64String(s);
        return Encoding.UTF8.GetString(data);
    }
}