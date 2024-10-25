using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OpenModular.Host.Web.OpenApi.Filters;
using OpenModular.Host.Web.Options;
using OpenModular.Module.Web;
using OpenModular.Module.Abstractions;
using Swashbuckle.AspNetCore.SwaggerGen;

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

            foreach (var descriptor in descriptors)
            {
                var module = descriptor.Module;

                c.SwaggerDoc(module.Code, new OpenApiInfo
                {
                    Title = module.Code,
                    Version = module.Version
                });

                LoadXml(descriptor, c);
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

            //转换TypeId
            c.SchemaFilter<TypeIdSchemaFilter>();
            c.OperationFilter<TypeIdOperationFilter>();
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
                op.SwaggerEndpoint($"{module.Code}/swagger.json", $"{module.Id:D3}-{module.Code}");
            }
        });

        return app;
    }

    private static void LoadXml(IModuleDescriptor descriptor, SwaggerGenOptions options)
    {
        var xmlFilePath = descriptor.Assemblies.Core.Location.Replace(".dll", ".xml", StringComparison.OrdinalIgnoreCase);
        if (File.Exists(xmlFilePath))
        {
            options.IncludeXmlComments(xmlFilePath);
        }

        if (descriptor.Assemblies.Web != null)
        {
            xmlFilePath = descriptor.Assemblies.Web.Location.Replace(".dll", ".xml", StringComparison.OrdinalIgnoreCase);
            if (File.Exists(xmlFilePath))
            {
                options.IncludeXmlComments(xmlFilePath);
            }
        }
    }
}