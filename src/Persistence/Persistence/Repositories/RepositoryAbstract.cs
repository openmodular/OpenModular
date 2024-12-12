using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Exceptions;
using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Persistence.Repositories;

public class RepositoryAbstract<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class, IEntity where TDbContext : EfDbContext<TDbContext>
{
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;
    public RepositoryAbstract(IDbContextProvider<TDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public Task<TDbContext> GetDbContextAsync()
    {
        return _dbContextProvider.GetDbContextAsync();
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return (await GetDbContextAsync()).Set<TEntity>().AsNoTracking();
    }

    public async Task<DbSet<TEntity>> GetDbSetAsync()
    {
        return (await GetDbContextAsync()).Set<TEntity>();
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var entity = await dbContext.Set<TEntity>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<TEntity>().Where(predicate).CountAsync(cancellationToken) > 0;
    }

    public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(predicate, false, cancellationToken);
    }

    public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        int count = await dbContext.Set<TEntity>().Where(predicate).ExecuteDeleteAsync(cancellationToken);

        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return count;
    }
}

public class RepositoryAbstract<TEntity, TKey, TDbContext>(IDbContextProvider<TDbContext> dbContextProvider) : RepositoryAbstract<TEntity, TDbContext>(dbContextProvider), IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : EfDbContext<TDbContext>
{
    public async Task<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var entity = await dbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id!);
        }

        return entity;
    }

    public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return InsertAsync(entity, false, cancellationToken);
    }

    public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }

    public Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return InsertManyAsync(entities, false, cancellationToken);
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        await dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return UpdateAsync(entity, false, cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        dbContext.Set<TEntity>().Update(entity);
        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }

    public Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return UpdateManyAsync(entities, false, cancellationToken);
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        dbContext.Set<TEntity>().UpdateRange(entities);
        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(id, false, cancellationToken);
    }

    public Task DeleteAsync(TKey id, bool autoSave, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(m => m.Id!.Equals(id), autoSave, cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(entity, false, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        dbContext.Set<TEntity>().Remove(entity);

        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return DeleteManyAsync(entities, false, cancellationToken);
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        dbContext.Set<TEntity>().RemoveRange(entities);

        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
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
        if (pagination.OrderBy!.NotNullOrWhiteSpace())
        {
            query = query.OrderBy(pagination.OrderBy!);
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