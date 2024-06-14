using Microsoft.Extensions.DependencyInjection;
using OpenModular.Common.Utils.Json;
using OpenModular.Module.Api;
using OpenModular.Module.Core;

namespace OpenModular.Host.Api;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加CORS
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
            所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
            第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
            Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
            //var preflightMaxAge = hostOptions.PreflightMaxAge > 0 ? new TimeSpan(0, 0, hostOptions.PreflightMaxAge) : new TimeSpan(0, 30, 0);

            options.AddPolicy("Default",
                builder => builder.SetIsOriginAllowed(_ => true)
                    //.SetPreflightMaxAge(preflightMaxAge)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
        });

        return services;
    }

    /// <summary>
    /// 配置JSON序列化选项
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleApiCollection"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services, IModuleApiCollection moduleApiCollection)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.AddCommonUtilsJsonSerializerContext();

            foreach (var moduleApi in moduleApiCollection)
            {
                options.SerializerOptions.AddJsonTypeInfoResolverFromAssembly(moduleApi.GetType().Assembly);
                options.SerializerOptions.AddJsonTypeInfoResolverFromAssembly(moduleApi.Module.GetType().Assembly);
            }
        });

        return services;
    }

    /// <summary>
    /// 添加OpenApi
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleApiCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularOpenApi(this IServiceCollection services, IModuleApiCollection moduleApiCollection)
    {
        foreach (var moduleApi in moduleApiCollection)
        {
            services.AddOpenApi(moduleApi.Module.Code.ToLower());
        }

        return services;
    }

    /// <summary>
    /// 添加MediatR
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleApiCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, IModuleApiCollection moduleApiCollection)
    {
        services.AddMediatR(cfg =>
        {
            foreach (var moduleApi in moduleApiCollection)
            {
                cfg.RegisterServicesFromAssembly(moduleApi.GetType().Assembly);
                cfg.RegisterServicesFromAssembly(moduleApi.Module.GetType().Assembly);
            }
        });

        return services;
    }

    /// <summary>
    /// 处理模块API的前置服务注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="moduleApiCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleApiPreConfigureService(this IServiceCollection services, ModuleConfigureContext context, IModuleApiCollection moduleApiCollection)
    {
        foreach (var moduleApi in moduleApiCollection)
        {
            moduleApi.PreConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 处理模块API的服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="moduleApiCollection"></param>
    /// <returns></returns>
    internal static IServiceCollection AddModuleApiConfigureService(this IServiceCollection services, ModuleConfigureContext context, IModuleApiCollection moduleApiCollection)
    {
        foreach (var moduleApi in moduleApiCollection)
        {
            moduleApi.ConfigureService(context);
        }
        return services;
    }

    /// <summary>
    /// 处理模块API的后置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="moduleApiCollection"></param>
    /// <returns></returns>
    internal static IServiceCollection AddModuleApiPostConfigureService(this IServiceCollection services, ModuleConfigureContext context, IModuleApiCollection moduleApiCollection)
    {
        foreach (var moduleApi in moduleApiCollection)
        {
            moduleApi.PostConfigureService(context);
        }
        return services;
    }
}