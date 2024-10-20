﻿using System.Linq.Expressions;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Exceptions;

namespace OpenModular.DDD.Core.Domain.Repositories;

public interface IRepository;

public interface IRepository<TEntity> : IRepository where TEntity : IEntity
{
    /// <summary>
    /// 获取一个查询器
    /// </summary>
    /// <returns></returns>
    Task<IQueryable<TEntity>> GetQueryableAsync();

    /// <summary>
    /// 根据指定的 <paramref name="predicate"/> 获取查询列表
    /// </summary>
    /// <param name="predicate">过滤条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有实体信息
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

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
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

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
    /// <param name="cancellationToken">取消令牌</param>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 删除多个实体
    /// </summary>
    /// <param name="predicate">删除条件</param>
    /// <param name="cancellationToken"></param>
    /// <returns>受影响的行数</returns>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据指定 <paramref name="predicate"/> 删除多个实体
    /// </summary>
    /// <param name="predicate">删除条件</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken"></param>
    /// <returns>受影响的行数</returns>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave, CancellationToken cancellationToken = default);
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
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    Task<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default);

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
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入一个实体
    /// </summary>
    /// <param name="entity">准备插入的实体</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> InsertAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入多个实体
    /// </summary>
    /// <param name="entities">准备插入的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 插入多个实体
    /// </summary>
    /// <param name="entities">准备插入的实体</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新一个实体
    /// </summary>
    /// <param name="entity">准备更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新一个实体
    /// </summary>
    /// <param name="entity">准备更新的实体</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task<TEntity> UpdateAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">准备更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新多个实体
    /// </summary>
    /// <param name="entities">准备更新的实体</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据标识删除一个实体
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据标识删除一个实体
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    Task DeleteAsync(TKey id, bool autoSave, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除一个实体
    /// </summary>
    /// <param name="entity">准备删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除一个实体
    /// </summary>
    /// <param name="entity">准备删除的实体</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">准备删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多个实体
    /// </summary>
    /// <param name="entities">准备删除的实体</param>
    /// <param name="autoSave">自动保存</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken = default);
}