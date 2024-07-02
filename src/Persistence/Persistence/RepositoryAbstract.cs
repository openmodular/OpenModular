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

    public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>().Where(predicate).ToList();
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>().Where(predicate).FirstOrDefault();
    }

    public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = DbContext.Set<TEntity>().Where(predicate).SingleOrDefault();
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity));

        return entity;
    }

    public bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>().Where(predicate).Any();
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().Where(predicate).CountAsync(cancellationToken) > 0;
    }

    public void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        DbContext.Set<TEntity>().Where(predicate).ExecuteDelete();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(predicate).ExecuteDeleteAsync(cancellationToken);
    }
}

public class RepositoryAbstract<TEntity, TKey, TDbContext>(IUnitOfWork unitOfWork) : RepositoryAbstract<TEntity, TDbContext>(unitOfWork), IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : OpenModularDbContext<TDbContext>
{
    public TEntity Find(TKey id)
    {
        return DbContext.Set<TEntity>().FirstOrDefault(m => m.Id!.Equals(id));
    }

    public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity Get(TKey id)
    {
        var entity = DbContext.Set<TEntity>().SingleOrDefault(m => m.Id!.Equals(id));
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        return entity;
    }

    public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().Where(m => m.Id!.Equals(id)).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        return entity;
    }

    public TEntity Insert(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);

        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public void InsertMany(IEnumerable<TEntity> entities)
    {
        DbContext.Set<TEntity>().AddRange(entities);
    }

    public Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public TEntity Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);

        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        return Task.FromResult(entity);
    }

    public void UpdateMany(IEnumerable<TEntity> entities)
    {
        DbContext.Set<TEntity>().UpdateRange(entities);
    }

    public Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().UpdateRange(entities);
        return Task.CompletedTask;
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public void DeleteMany(IEnumerable<TEntity> entities)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);
    }

    public Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);

        return Task.CompletedTask;
    }
}