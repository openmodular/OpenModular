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
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi().CacheOutput();

            var moduleWebCollection = app.Services.GetRequiredService<IModuleWebCollection>();

            foreach (var descriptor in moduleWebCollection)
            {
                app.UseSwaggerUI(options =>
                {
                    var moduleCode = descriptor.ModuleWeb.Module.Code;
                    options.SwaggerEndpoint($"/openapi/{moduleCode.ToLower()}.json", moduleCode);
                });
            }
        }

        return app;
    }
}