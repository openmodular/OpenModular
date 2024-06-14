using Microsoft.EntityFrameworkCore;

namespace OpenModular.Persistence
{
    public abstract class DbMigrationProviderAbstract<TDbContext>(TDbContext context)
        : IDbMigrationProvider, IDisposable
        where TDbContext : OpenModularDbContext<TDbContext>
    {
        public virtual async Task SchemaMigrateAsync()
        {
            if (!(await context.Database.GetPendingMigrationsAsync()).Any())
                return;

            await context.Database.MigrateAsync();
        }

        public virtual Task DataMigrateAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
