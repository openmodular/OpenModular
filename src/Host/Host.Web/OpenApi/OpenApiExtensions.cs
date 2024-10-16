using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OpenModular.Host.Web.OpenApi.Filters;
using OpenModular.Host.Web.Options;
using OpenModular.Module.Web;

namespace OpenModular.Host.Web.OpenApi;

public static class OpenApiExtensions
{
    /// <summary>
    /// 添加OpenApi服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularOpenApi(this IServiceCollection services, OpenApiOptions options)
    {
        if (!options.Enable)
            return services;

        services.AddSwaggerGen(c =>
        {
            var descriptors = services.GetModuleCollection();

            if (descriptors != null)
            {
                foreach (var descriptor in descriptors)
                {
                    var module = descriptor.Module;

                    c.SwaggerDoc(module.Code.ToLower(), new OpenApiInfo
                    {
                        Title = module.Code,
                        Version = module.Version
                    });
                }
            }

            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };

            //添加设置Token的按钮
            c.AddSecurityDefinition("Bearer", securityScheme);

            //添加Jwt验证设置
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            //启用注解
            c.EnableAnnotations();

            //隐藏属性
            c.SchemaFilter<SwaggerIgnoreSchemaFilter>();
            c.OperationFilter<SwaggerIgnoreOperationFilter>();
        });


        return services;
    }

    /// <summary>
    /// 添加OpenApi映射
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static WebApplication UseOpenModularOpenApi(this WebApplication app, OpenApiOptions options)
    {
        if (!options.Enable)
            return app;

        app.UseSwagger();
        app.UseSwaggerUI(op =>
        {
            var moduleWebCollection = app.Services.GetRequiredService<IModuleWebCollection>().OrderBy(m => m.ModuleWeb.Module.Id);

            foreach (var descriptor in moduleWebCollection)
            {
                var module = descriptor.ModuleWeb.Module;
                op.SwaggerEndpoint($"{module.Code.ToLower()}/swagger.json", $"{module.Id}_{module.Code}");
            }
        });

        return app;
    }
}