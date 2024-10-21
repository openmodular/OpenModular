using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Persistence.Uow;

public class UnitOfWork(IDbContextBuilder dbContextBuilder, IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly List<DbContext> _dbContexts = new();
    private IDbContextTransaction? _transaction;
    private DbConnection? _connection;
    private string? _databaseProviderName;

    public event EventHandler<UnitOfWorkEventArgs>? Disposed;

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

    public async Task<TDbContext> GetDbContextAsync<TDbContext>(CancellationToken cancellationToken = default) where TDbContext : EfDbContext<TDbContext>
    {
        var dbContext = _dbContexts.OfType<TDbContext>().FirstOrDefault();
        if (dbContext == null)
        {
            if (_connection == null)
            {
                dbContext = serviceProvider.GetRequiredService<TDbContext>();
                _connection = dbContext.Database.GetDbConnection();
                _databaseProviderName = dbContext.Database.ProviderName;
            }
            else
            {
                dbContext = dbContextBuilder.Build<TDbContext>(_databaseProviderName!, _connection);
            }

            _dbContexts.Add(dbContext);

            //Sqlite不支持事务
            if (dbContext.GetDatabaseProvider() == DbProvider.Sqlite)
            {
                return dbContext;
            }

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

        Disposed?.Invoke(this, new UnitOfWorkEventArgs(this));
    }
}