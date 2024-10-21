using Microsoft.EntityFrameworkCore;

namespace OpenModular.Persistence;

public abstract class DbMigrationProviderAbstract<TDbContext>(TDbContext context) : IDbMigrationProvider, IDisposable where TDbContext : EfDbContext<TDbContext>
{
    public virtual async Task SchemaMigrateAsync()
    {
        if (!(await context.Database.GetPendingMigrationsAsync()).Any())
            return;

        await context.Database.MigrateAsync();
        await context.Database.EnsureCreatedAsync();
    }

    public virtual Task DataMigrateAsync()
    {
        return Task.CompletedTask;
    }

    public virtual void Dispose()
    {
        context.Dispose();
    }
}