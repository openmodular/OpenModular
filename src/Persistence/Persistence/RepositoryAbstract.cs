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

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var db = DbContext.Set<TEntity>();
        var entities = await db.Where(predicate).ToListAsync(cancellationToken);
        db.RemoveRange(entities);
    }
}

public class RepositoryAbstract<TEntity, TKey, TDbContext>(IUnitOfWork unitOfWork) : RepositoryAbstract<TEntity, TDbContext>(unitOfWork), IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : OpenModularDbContext<TDbContext>
{
    public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        return Task.FromResult(entity);
    }

    public Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().UpdateRange(entities);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);

        return Task.CompletedTask;
    }
}