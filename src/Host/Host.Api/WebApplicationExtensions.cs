using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Api;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;

namespace OpenModular.Host.Api
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseOpenModularEndpoints(this WebApplication app)
        {
            var moduleApiCollection = app.Services.GetRequiredService<IModuleApiCollection>();

            foreach (var moduleApi in moduleApiCollection)
            {
                var endpointTypes = moduleApi.GetType().Assembly.GetTypes()
                    .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();

                var group = app.MapGroup(moduleApi.Module.Code.ToLower());

                foreach (var endpoint in endpointTypes)
                {
                    var endpointInstance = (IEndpoint)Activator.CreateInstance(endpoint)!;

                    endpointInstance.Mapping(TypeAdapterConfig.GlobalSettings);

                    endpointInstance.RouteMap(group, app);
                }
            }

            return app;
        }

        public static WebApplication UseOpenApi(this WebApplication app)
        {
            var moduleApiCollection = app.Services.GetRequiredService<IModuleApiCollection>();

            app.MapOpenApi().CacheOutput();

            if (app.Environment.IsDevelopment())
            {
                foreach (var moduleApi in moduleApiCollection)
                {
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint($"/openapi/{moduleApi.Module.Code.ToLower()}.json", "v1");
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
