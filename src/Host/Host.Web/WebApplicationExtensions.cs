using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;

namespace OpenModular.Host.Web;

public static class WebApplicationExtensions
{
    public static WebApplication ExecuteDbMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<DbMigrationHandler>();
        handler.MigrateAsync().GetAwaiter().GetResult();

        return app;
    }

    public static WebApplication ExecuteDataSeeding(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var executor = scope.ServiceProvider.GetRequiredService<IDataSeedingExecutor>();
        executor.ExecuteAsync().GetAwaiter().GetResult();

        return app;
    }
}