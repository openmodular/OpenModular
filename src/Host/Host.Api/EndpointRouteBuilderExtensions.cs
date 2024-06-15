using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Api;

namespace OpenModular.Host.Api
{
    public static class EndpointRouteBuilderExtensions
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

            app.MapOpenApi();

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
    }
}
