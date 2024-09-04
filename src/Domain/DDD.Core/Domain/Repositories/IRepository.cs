using System.Linq.Expressions;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Exceptions;

namespace OpenModular.DDD.Core.Domain.Repositories;

public interface IRepository;

public interface IRepository<TEntity> : IRepository where TEntity : IEntity
{
    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetQueryable();

    /// <summary>
    /// 根据指定的 <paramref name="predicate"/> 获取查询列表
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据指定的 <paramref name="predicate"/> 获取查询列表
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

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
    TEntity Find(Expression<Func<TEntity, bool>> predicate);

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
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

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
    TEntity Get(Expression<Func<TEntity, bool>> predicate);

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
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 判断实体是否存在
    /// <para>
    /// 如果查询不到数据则抛出异常 <see cref="EntityNotFoundException"></see>
    /// </para>
    /// <para>
    /// 如果查询到多条数据则抛出异常 <see cref="InvalidOperationException "></see>
    /// </para>
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    bool Exists(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 判断实体是否存在
    /// <para>
    /// 如果查询不到数据则抛出异常 <see cref="EntityNotFoundException"></see>
    /// </para>
    /// <para>
    /// 如果查询到多条数据则抛出异常 <see cref="InvalidOperationException "></see>
    /// </para>
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 删除多个实体
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    void Delete(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 删除多个实体
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity, in TKey> : IRepository<TEntity> where TEntity : IEntity<TKey>
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
    /// <returns></returns>
    TEntity Find(TKey id);

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
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default);

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
    /// <returns></returns>
    TEntity Get(TKey id);

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
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入一个实体
    /// </summary>
    /// <param name="entity">准备插入的实体</param>
    TEntity Insert(TEntity entity);

    /// <summary>
    /// 插入一个实体
    /// </summary>
    /// <param name="entity">准备插入的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入多个实体
    /// </summary>
    /// <param name="entities">准备插入的实体</param>
    void InsertMany(IEnumerable<TEntity> entities);

    /// <summary>
    /// 插入多个实体
    /// </summary>
    /// <param name="entities">准备插入的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新一个实体
    /// </summary>
    /// <param name="entity">准备更新的实体</param>
    TEntity Update(TEntity entity);

    /// <summary>
    /// 更新一个实体
    /// </summary>
    /// <param name="entity">准备更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">准备更新的实体</param>
    void UpdateMany(IEnumerable<TEntity> entities);

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">准备更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除一个实体
    /// </summary>
    /// <param name="entity">准备删除的实体</param>
    void Delete(TEntity entity);

    /// <summary>
    /// 删除一个实体
    /// </summary>
    /// <param name="entity">准备删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">准备删除的实体</param>
    void DeleteMany(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">准备删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}