﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Host.Web.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加CORS
    /// </summary>
    /// <param name="services"></param>
    /// <param name="hostOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularCors(this IServiceCollection services, WebHostOptions hostOptions)
    {
        services.AddCors(options =>
        {
            /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
            所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
            第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
            Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
            var preflightMaxAge = hostOptions.PreflightMaxAge > 0 ? new TimeSpan(0, 0, hostOptions.PreflightMaxAge) : new TimeSpan(0, 30, 0);

            options.AddPolicy("Default",
                builder => builder.SetIsOriginAllowed(_ => true)
                    .SetPreflightMaxAge(preflightMaxAge)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
        });

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

    /// <summary>
    /// 添加多语言服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization(opt => opt.ResourcesPath = "Resources");

        services.Configure<LangOptions>(configuration.GetSection(LangOptions.Position));

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var langOptions = services.BuildServiceProvider().GetRequiredService<IOptions<LangOptions>>().Value;

            var supportedCultures = langOptions.Supported.IsNullOrEmpty() ? ["zh-CN", "en-US"] : langOptions.Supported;
            var defaultCulture = langOptions.DefaultLang.IsNullOrWhiteSpace() ? supportedCultures[0] : langOptions.DefaultLang;

            options.SetDefaultCulture(defaultCulture)
                .AddSupportedCultures(supportedCultures);
        });

        return services;
    }
}