using System.Linq.Expressions;

namespace OpenModular.Common.Utils.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// 如果条件为true则添加 <paramref name="predicate"/> 条件
    /// </summary>
    /// <param name="query"></param>
    /// <param name="condition">添加条件</param>
    /// <param name="predicate">条件</param>
    /// <returns></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        if (condition)
        {
            return query.Where(predicate);
        }

        return query;
    }

    /// <summary>
    /// 如果条件为true则添加 <paramref name="ifPredicate"/> 条件，反之添加 <paramref name="elsePredicate"/> 条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    /// <param name="ifPredicate"></param>
    /// <param name="elsePredicate"></param>
    /// <returns></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> ifPredicate, Expression<Func<T, bool>> elsePredicate)
    {
        if (condition)
        {
            return query.Where(ifPredicate);
        }

        return query.Where(elsePredicate);
    }

    /// <summary>
    /// 如果字符串不为null或者空字符串则添加 <paramref name="predicate"/> 条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="value">条件</param>
    /// <param name="predicate">过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> query, string? value, Expression<Func<T, bool>> predicate)
    {
        if (value!.IsNull())
            return query;

        return query.Where(predicate);
    }

    /// <summary>
    /// 如果字符串不为null或空字符串则添加 <paramref name="ifPredicate"/> 条件，反之添加 <paramref name="elsePredicate"/> 条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="value">条件</param>
    /// <param name="ifPredicate">字符串不为null或空字符串时应用的过滤条件</param>
    /// <param name="elsePredicate">字符串为null或空字符串时应用的过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> query, string? value, Expression<Func<T, bool>> ifPredicate, Expression<Func<T, bool>> elsePredicate)
    {
        if (value!.IsNull())
        {
            return query.Where(elsePredicate);
        }

        return query.Where(ifPredicate);
    }

    /// <summary>
    /// 如果对象不为null则添加 <paramref name="predicate"/> 条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="obj">条件</param>
    /// <param name="predicate">过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotNull<T, TObject>(this IQueryable<T> query, TObject? obj, Expression<Func<T, bool>> predicate)
    {
        if (obj == null)
            return query;
        return query.Where(predicate);
    }

    /// <summary>
    /// 如果对象不为null则添加 <paramref name="ifPredicate"/> 条件，反之添加 <paramref name="elsePredicate"/> 条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="query"></param>
    /// <param name="obj"></param>
    /// <param name="ifPredicate"></param>
    /// <param name="elsePredicate"></param>
    /// <returns></returns>
    public static IQueryable<T> WhereNotNull<T, TObject>(this IQueryable<T> query, TObject? obj, Expression<Func<T, bool>> ifPredicate, Expression<Func<T, bool>> elsePredicate)
    {
        if (obj == null)
        {
            return query.Where(elsePredicate);
        }

        return query.Where(ifPredicate);
    }

    /// <summary>
    /// 如果Guid不为 null 或者 Guid.Empty 则添加 <paramref name="predicate"/> 条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="predicate">过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotEmpty<T>(this IQueryable<T> query, Guid? condition, Expression<Func<T, bool>> predicate)
    {
        if (condition == null || condition == Guid.Empty)
            return query;

        return query.Where(predicate);
    }

    /// <summary>
    /// 如果Guid不为 null 或者 Guid.Empty 则添加 <paramref name="ifPredicate"/> 条件，反之添加 <paramref name="elsePredicate"/> 条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="ifPredicate">字符串不为null或空字符串时应用的过滤条件</param>
    /// <param name="elsePredicate">字符串为null或空字符串时应用的过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotEmpty<T>(this IQueryable<T> query, Guid? condition, Expression<Func<T, bool>> ifPredicate, Expression<Func<T, bool>> elsePredicate)
    {
        if (condition == null || condition == Guid.Empty)
            return query.Where(elsePredicate);

        return query.Where(ifPredicate);
    }
}