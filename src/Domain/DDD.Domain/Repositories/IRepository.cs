using OpenModular.DDD.Domain.Entities;
using System.Linq.Expressions;
using OpenModular.DDD.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace OpenModular.DDD.Domain.Repositories;

public interface IRepository;

public interface IRepository<TEntity> : IRepository where TEntity : IEntity
{
    /// <summary>
    /// 根据指定的 <paramref name="predicate"/> 获取查询列表
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    /// <param name="includeDetails">包含详情</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 获取单个实体信息
    /// <para>
    /// 如果查询不到数据则返回 null
    /// </para>
    /// <para>
    /// 如果查询到多条数据则抛出异常 <see cref="InvalidOperationException "></see>
    /// </para>
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    /// <param name="includeDetails">包含详情</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 获取单个实体信息
    /// <para>
    /// 如果查询不到数据则抛出异常 <see cref="EntityNotFoundException"></see>
    /// </para>
    /// <para>
    /// 如果查询到多条数据则抛出异常 <see cref="InvalidOperationException "></see>
    /// </para>
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    /// <param name="includeDetails">包含详情</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 删除多个实体
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity, in TKey> : IRepository where TEntity : IEntity<TKey>
{
    /// <summary>
    /// 根据  <paramref name="id"/> 获取实体信息
    /// <para>
    /// 如果查询不到数据则返回 null
    /// </para>
    /// <para>
    /// 如果查询到多条数据则抛出异常 <see cref="InvalidOperationException "></see>
    /// </para>
    /// </summary>
    /// <param name="id">实体标识</param>
    /// <param name="includeDetails">包含详情</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据  <paramref name="id"/> 获取实体信息
    /// <para>
    /// 如果查询不到数据则抛出异常 <see cref="EntityNotFoundException"></see>
    /// </para>
    /// <para>
    /// 如果查询到多条数据则抛出异常 <see cref="InvalidOperationException "></see>
    /// </para>
    /// </summary>
    /// <param name="id">实体标识</param>
    /// <param name="includeDetails">包含详情</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入一个实体
    /// </summary>
    /// <param name="entity">准备插入的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> InsertAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入多个实体
    /// </summary>
    /// <param name="entities">准备插入的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新一个实体
    /// </summary>
    /// <param name="entity">准备更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> UpdateAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">准备更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除一个实体
    /// </summary>
    /// <param name="entity">准备删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">准备删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}