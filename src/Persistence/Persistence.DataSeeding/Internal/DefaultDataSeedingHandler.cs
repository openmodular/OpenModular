using LiteDB;

namespace OpenModular.Persistence.DataSeeding.Internal;

internal class DefaultDataSeedingHandler<TDbContext>(TDbContext dbContext, IDataSeedingHistory history) : IDataSeedingHandler where TDbContext : OpenModularDbContext<TDbContext>
{
    public async Task DoAsync()
    {
        var moduleCode = dbContext.Module.Code;

        var lastVersion = await history.GetLastVersionAsync(moduleCode);

        using var db = new LiteDatabase(new ConnectionString
        {
            Filename = _dbFilePath,
            Password = DataMigrationConstant.DbPassword
        });
    }
}