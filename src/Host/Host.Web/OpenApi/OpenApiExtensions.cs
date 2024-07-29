using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Core;
using OpenModular.Module.Web;

namespace OpenModular.Host.Web.OpenApi;

public static class OpenApiExtensions
{
    /// <summary>
    /// 添加OpenApi服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularOpenApi(this IServiceCollection services)
    {
        var descriptors = services.GetModuleCollection();

        foreach (var descriptor in descriptors)
        {
            services.AddOpenApi(descriptor.Module.Code.ToLower());
        }

        return services;
    }

    /// <summary>
    /// 添加OpenApi映射
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseOpenModularOpenApi(this WebApplication app)
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
}