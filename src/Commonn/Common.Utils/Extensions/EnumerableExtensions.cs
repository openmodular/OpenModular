﻿namespace OpenModular.Common.Utils.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// 判断集合为null或者不包含任何元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }
}