using Microsoft.Extensions.DependencyInjection;
using OpenModular.DDD.Core.Uow;
using OpenModular.Persistence.Exceptions;
using OpenModular.Persistence.Uow;

namespace OpenModular.Persistence;

internal class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext> where TDbContext : EfDbContext<TDbContext>
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IServiceProvider _serviceProvider;

    public DbContextProvider(IUnitOfWorkManager unitOfWorkManager, IServiceProvider serviceProvider)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _serviceProvider = serviceProvider;
    }

    public async Task<TDbContext> GetDbContextAsync()
    {
        if (_unitOfWorkManager.Current == null)
        {
            return _serviceProvider.GetRequiredService<TDbContext>();
        }

        var unitOfWork = _unitOfWorkManager.Current as UnitOfWork;
        if (unitOfWork == null)
        {
            throw new PersistenceException("A DbContext can only be created inside a unit of work!");
        }

        var dbContext = await unitOfWork.GetDbContextAsync<TDbContext>();

        return dbContext;
    }
}