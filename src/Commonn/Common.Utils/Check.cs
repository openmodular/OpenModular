﻿
// ReSharper disable once CheckNamespace
namespace OpenModular;

public static class Check
{
    /// <summary>
    /// 字符串不为 null, 空或者空白(等同于string.IsNullOrWhiteSpace)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NullOrWhiteSpace(string? value, string parameterName)
    {
        return NullOrWhiteSpace(value, parameterName, Int32.MaxValue, 0);
    }

    /// <summary>
    /// 字符串不为 null, 空或者空白(等同于string.IsNullOrWhiteSpace)，或者指定长度范围
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NullOrWhiteSpace(string? value, string parameterName, int maxLength, int minLength)
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

    /// <summary>
    /// value不为null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T NotNull<T>(T? value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentException($"{parameterName} can not be null!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保给定的集合不为null或者空集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<T> NotNullOrEmpty<T>(IEnumerable<T>? collection, string parameterName)
    {
        if (collection == null || !collection.Any())
        {
            throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
        }

        return collection;
    }

    /// <summary>
    /// 确保前值小于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static int LessThan(int value, int maxValue, string parameterName)
    {
        if (value >= maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static decimal LessThan(decimal value, decimal maxValue, string parameterName)
    {
        if (value >= maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static double LessThan(double value, double maxValue, string parameterName)
    {
        if (value >= maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static float LessThan(float value, float maxValue, string parameterName)
    {
        if (value >= maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于等于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static int LessThanOrEqual(int value, int maxValue, string parameterName)
    {
        if (value > maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than or equal to {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于等于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static decimal LessThanOrEqual(decimal value, decimal maxValue, string parameterName)
    {
        if (value > maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than or equal to {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于等于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static double LessThanOrEqual(double value, double maxValue, string parameterName)
    {
        if (value > maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than or equal to {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值小于等于后值
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static float LessThanOrEqual(float value, float maxValue, string parameterName)
    {
        if (value > maxValue)
        {
            throw new ArgumentException($"{parameterName} must be less than or equal to {maxValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于后值
    /// </summary>
    public static int GreaterThan(int value, int minValue, string parameterName)
    {
        if (value <= minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于后值
    /// </summary>
    public static decimal GreaterThan(decimal value, decimal minValue, string parameterName)
    {
        if (value <= minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于后值
    /// </summary>
    public static double GreaterThan(double value, double minValue, string parameterName)
    {
        if (value <= minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于后值
    /// </summary>
    public static float GreaterThan(float value, float minValue, string parameterName)
    {
        if (value <= minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于等于后值
    /// </summary>
    public static int GreaterThanOrEqual(int value, int minValue, string parameterName)
    {
        if (value < minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than or equal to {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于等于后值
    /// </summary>
    public static decimal GreaterThanOrEqual(decimal value, decimal minValue, string parameterName)
    {
        if (value < minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than or equal to {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于等于后值
    /// </summary>
    public static double GreaterThanOrEqual(double value, double minValue, string parameterName)
    {
        if (value < minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than or equal to {minValue}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 确保前值大于等于后值
    /// </summary>
    public static float GreaterThanOrEqual(float value, float minValue, string parameterName)
    {
        if (value < minValue)
        {
            throw new ArgumentException($"{parameterName} must be greater than or equal to {minValue}!", parameterName);
        }

        return value;
    }
}