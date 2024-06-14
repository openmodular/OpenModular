using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Persistence;

internal class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly List<DbContext> _dbContexts = new();
    private IDbContextTransaction? _transaction;

    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContexts.Any())
        {
            foreach (var dbContext in _dbContexts)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
    }

    public async Task<TDbContext> GetDbContextAsync<TDbContext>(CancellationToken cancellationToken = default) where TDbContext : DbContext
    {
        var dbContext = _dbContexts.OfType<TDbContext>().FirstOrDefault();
        if (dbContext == null)
        {
            dbContext = serviceProvider.GetRequiredService<TDbContext>();
            _dbContexts.Add(dbContext);

            if (_transaction == null)
            {
                _transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            }
            else
            {
                await dbContext.Database.UseTransactionAsync(_transaction.GetDbTransaction(), cancellationToken);
            }
        }

        return dbContext;
    }

    public virtual void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
        }
    }
}