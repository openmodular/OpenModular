using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Web;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;

namespace OpenModular.Host.Web
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseOpenApi(this WebApplication app)
        {
            var moduleWebCollection = app.Services.GetRequiredService<IModuleWebCollection>();

            app.MapOpenApi().CacheOutput();

            if (app.Environment.IsDevelopment())
            {
                foreach (var descriptor in moduleWebCollection)
                {
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint($"/openapi/{descriptor.ModuleWeb.Module.Code.ToLower()}.json", "v1");
                    });
                }
            }

            return app;
        }

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
}
