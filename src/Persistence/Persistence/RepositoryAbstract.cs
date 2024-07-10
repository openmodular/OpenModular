using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Exceptions;
using OpenModular.DDD.Core.Domain.Repositories;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Persistence;

public class RepositoryAbstract<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class, IEntity where TDbContext : OpenModularDbContext<TDbContext>
{
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TEntity> Db;

    public RepositoryAbstract(IUnitOfWork unitOfWork)
    {
        var uow = unitOfWork as UnitOfWork;
        if (uow == null)
            throw new Exception("Invalid UnitOfWork");

        DbContext = uow.GetDbContextAsync<TDbContext>().Result;
        Db = DbContext.Set<TEntity>();
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        return Db.Where(predicate).ToList();
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Db.Where(predicate).ToListAsync(cancellationToken);
    }

    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Db.Where(predicate).FirstOrDefault();
    }

    public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Db.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = Db.Where(predicate).SingleOrDefault();
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await Db.Where(predicate).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return Db.Where(predicate).Any();
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Db.Where(predicate).CountAsync(cancellationToken) > 0;
    }

    public void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        Db.Where(predicate).ExecuteDelete();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Db.Where(predicate).ExecuteDeleteAsync(cancellationToken);
    }
}

public class RepositoryAbstract<TEntity, TKey, TDbContext>(IUnitOfWork unitOfWork) : RepositoryAbstract<TEntity, TDbContext>(unitOfWork), IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : OpenModularDbContext<TDbContext>
{
    public TEntity Find(TKey id)
    {
        return Db.FirstOrDefault(m => m.Id!.Equals(id));
    }

    public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return Db.Where(m => m.Id!.Equals(id)).FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity Get(TKey id)
    {
        var entity = Db.SingleOrDefault(m => m.Id!.Equals(id));
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        return entity;
    }

    public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await Db.Where(m => m.Id!.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        return entity;
    }

    public TEntity Insert(TEntity entity)
    {
        Db.Add(entity);

        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Db.AddAsync(entity, cancellationToken);

        return entity;
    }

    public void InsertMany(IEnumerable<TEntity> entities)
    {
        Db.AddRange(entities);
    }

    public Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return Db.AddRangeAsync(entities, cancellationToken);
    }

    public TEntity Update(TEntity entity)
    {
        Db.Update(entity);

        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Db.Update(entity);

        return Task.FromResult(entity);
    }

    public void UpdateMany(IEnumerable<TEntity> entities)
    {
        Db.UpdateRange(entities);
    }

    public Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Db.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public void Delete(TEntity entity)
    {
        Db.Remove(entity);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Db.Remove(entity);
        return Task.CompletedTask;
    }

    public void DeleteMany(IEnumerable<TEntity> entities)
    {
        Db.RemoveRange(entities);
    }

    public Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Db.RemoveRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    protected async Task<Common.Utils.Paging.PagedResult<T>> ToPagedAsync<T>(IQueryable<T> query, Pagination pagination)
    {
        if (pagination.OrderBy.NotNullAndWhiteSpace())
        {
            query = query.OrderBy(pagination.OrderBy);
        }

        var rows = await query.Skip((pagination.Index - 1) * pagination.Size).Take(pagination.Size).ToListAsync();
        if (rows.Count < pagination.Size && pagination.Index <= 1)
        {
            return new Common.Utils.Paging.PagedResult<T>(rows, rows.Count, pagination.Index, pagination.Size);
        }

        var total = await query.CountAsync();

        return new Common.Utils.Paging.PagedResult<T>(rows, total, pagination.Index, pagination.Size);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    protected Task<Common.Utils.Paging.PagedResult<TEntity>> ToPagedAsync(IQueryable<TEntity> query, Pagination pagination)
    {
        return ToPagedAsync<TEntity>(query, pagination);
    }
}