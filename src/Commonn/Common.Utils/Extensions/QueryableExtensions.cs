using System.Linq.Expressions;

namespace OpenModular.Common.Utils.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// 条件为true时应用过滤条件
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
    /// 条件为true时应用第一个过滤条件，反之应用第二个过滤条件
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
    /// 如果指定的条件不为null或者空字符串则应用过滤条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="predicate">过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> query, string condition, Expression<Func<T, bool>> predicate)
    {
        if (condition.IsNull())
            return query;

        return query.Where(predicate);
    }

    /// <summary>
    /// 如果字符串不为null或空字符串则应用第一个条件，反之使用第二个条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="ifPredicate">字符串不为null或空字符串时应用的过滤条件</param>
    /// <param name="elsePredicate">字符串为null或空字符串时应用的过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> query, string condition, Expression<Func<T, bool>> ifPredicate, Expression<Func<T, bool>> elsePredicate)
    {
        if (condition.IsNull())
        {
            return query.Where(elsePredicate);
        }

        return query.Where(ifPredicate);
    }

    /// <summary>
    /// 如果Guid不为空则应用过滤条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="predicate">过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotEmpty<T>(this IQueryable<T> query, Guid condition, Expression<Func<T, bool>> predicate)
    {
        if (condition == Guid.Empty)
            return query;

        return query.Where(predicate);
    }

    /// <summary>
    /// 如果Guid不为空则应用第一个条件，反之使用第二个条件
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="ifPredicate">字符串不为null或空字符串时应用的过滤条件</param>
    /// <param name="elsePredicate">字符串为null或空字符串时应用的过滤条件</param>
    /// <returns>过滤后的查询对象</returns>
    public static IQueryable<T> WhereNotEmpty<T>(this IQueryable<T> query, Guid condition, Expression<Func<T, bool>> ifPredicate, Expression<Func<T, bool>> elsePredicate)
    {
        if (condition == Guid.Empty)
            return query.Where(elsePredicate);

        return query.Where(ifPredicate);
    }
}