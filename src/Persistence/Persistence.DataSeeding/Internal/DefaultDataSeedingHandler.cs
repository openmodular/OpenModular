namespace OpenModular.Persistence.DataSeeding.Internal;

internal class DefaultDataSeedingHandler<TDbContext> : IDataSeedingHandler where TDbContext : OpenModularDbContext<TDbContext>
{
    private readonly TDbContext _dbContext;

    public DefaultDataSeedingHandler(TDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public Task DoAsync()
    {
        throw new NotImplementedException();
    }
}