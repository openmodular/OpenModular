using Microsoft.Extensions.DependencyInjection;
using OpenModular.Host.Web.Middlewares;
using OpenModular.Module.Core;
using OpenModular.Module.Web;

namespace OpenModular.Host.Web;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenModularMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandleMiddleware>();
        services.AddScoped<UnitOfWorkMiddleware>();

        return services;
    }

    /// <summary>
    /// 添加CORS
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularCors(this IServiceCollection services)
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
    /// 添加OpenApi
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
    /// 添加MediatR
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            var descriptors = services.GetModuleWebCollection();

            foreach (var descriptor in descriptors)
            {
                cfg.RegisterServicesFromAssembly(descriptor.ModuleWeb.GetType().Assembly);
                cfg.RegisterServicesFromAssembly(descriptor.ModuleWeb.Module.GetType().Assembly);
            }
        });

        return services;
    }
}