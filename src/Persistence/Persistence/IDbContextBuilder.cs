using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace OpenModular.Persistence;

public interface IDbContextBuilder
{
    TDbContext Build<TDbContext>(string databaseProviderName, DbConnection connection) where TDbContext : EfDbContext<TDbContext>;
}

internal class DbContextBuilder : IDbContextBuilder
{
    public TDbContext Build<TDbContext>(string databaseProviderName, DbConnection connection) where TDbContext : EfDbContext<TDbContext>
    {
        DbContextOptions<TDbContext> options = null;
        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

        switch (databaseProviderName)
        {
            case DbConstants.PostgreSQLProviderName:
                options = optionsBuilder.UseNpgsql(connection).Options;
                break;
            case DbConstants.SqlServerProviderName:
                options = optionsBuilder.UseSqlServer(connection).Options;
                break;
            case DbConstants.SqliteProviderName:
                options = optionsBuilder.UseSqlite(connection).Options;
                break;
        }

        if (options == null)
        {
            throw new NotSupportedException($"The database({databaseProviderName}) not supported");
        }

        var dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
        return dbContext;
    }
}