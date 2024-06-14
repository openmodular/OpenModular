using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Exceptions;
using OpenModular.DDD.Core.Domain.Repositories;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Persistence;

public class RepositoryAbstract<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class, IEntity where TDbContext : OpenModularDbContext<TDbContext>
{
    protected readonly TDbContext DbContext;

    public RepositoryAbstract(IUnitOfWork unitOfWork)
    {
        var uow = unitOfWork as UnitOfWork;
        if (uow == null)
            throw new Exception("Invalid UnitOfWork");

        DbContext = uow.GetDbContextAsync<TDbContext>().Result;
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var db = DbContext.Set<TEntity>();
        var entities = await db.Where(predicate).ToListAsync(cancellationToken);
        db.RemoveRange(entities);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

public class RepositoryAbstract<TEntity, TKey, TDbContext>(IUnitOfWork unitOfWork) : RepositoryAbstract<TEntity, TDbContext>(unitOfWork), IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : OpenModularDbContext<TDbContext>
{
    public Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().UpdateRange(entities);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);

        if (autoSave)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}