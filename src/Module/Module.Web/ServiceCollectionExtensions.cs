﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.Abstractions;
using OpenModular.Module.Core;
using OpenModular.Module.Web;
using OpenModular.Module.Web.Conventions;
using OpenModular.Module.Web.ModelBinderProviders;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加模块化Web服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModuleWebService(this IServiceCollection services)
    {
        services.AddModuleCoreService();

        services.AddSingleton<IModuleWebCollection>(new ModuleWebCollection());

        services.TryAddTransient<ITenantResolver, TenantResolver>();

        services.AddHttpContextAccessor();

        services.TryAddSingleton<ICurrentAccount, CurrentAccount>();

        return services;
    }

    /// <summary>
    /// 获取 IModuleWebCollection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IModuleWebCollection GetModuleWebCollection(this IServiceCollection services)
    {
        return (IModuleWebCollection)services.First(m => m.ServiceType == typeof(IModuleWebCollection)).ImplementationInstance!;
    }

    /// <summary>
    /// 注册模块
    /// </summary>
    /// <param name="services"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterModule(this IServiceCollection services, IModule module)
    {
        services.RegisterModuleCore(module);
        return services;
    }

    /// <summary>
    /// 注册模块Web
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleWeb"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterModuleWeb(this IServiceCollection services, IModuleWeb moduleWeb)
    {
        var moduleDescriptor = services.RegisterModuleCore(moduleWeb.Module);

        var collection = services.GetModuleWebCollection();
        collection.Add(new ModuleWebDescriptor(moduleWeb));

        moduleDescriptor.Assemblies.Web = moduleWeb.GetType().Assembly;

        return services;
    }

    /// <summary>
    /// 添加MVC功能
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IMvcBuilder AddOpenModularMvc(this IServiceCollection services)
    {
        var builder = services.AddControllersWithViews(c =>
        {
            //API分组约定
            c.Conventions.Add(new ApiExplorerGroupConvention());

            //TypeId绑定
            c.ModelBinderProviders.Insert(0, new TypeIdBinderProvider());
        });

        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new AccountIdJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new TenantIdJsonConverter());
        });

        var descriptors = services.GetModuleWebCollection();

        foreach (var descriptor in descriptors)
        {
            var des = descriptor as ModuleWebDescriptor;
            des?.Configurator?.ConfigureMvc(builder);
        }

        return builder;
    }

    /// <summary>
    /// 处理模块Web的前置服务注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleWebPreConfigureService(this IServiceCollection services, ModuleConfigureContext context)
    {
        services.AddModulePreService(context);

        var collection = services.GetModuleWebCollection();
        foreach (var descriptor in collection!)
        {
            var assembly = descriptor.ModuleWeb.GetType().Assembly;

            services.AddAutoMapper(assembly);

            context.Services.AddServicesFromAssembly(assembly);

            var des = descriptor as ModuleWebDescriptor;
            des?.Configurator?.PreConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 处理模块Web的服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleWebConfigureService(this IServiceCollection services, ModuleConfigureContext context)
    {
        services.AddModuleService(context);

        var collection = services.GetModuleWebCollection();
        foreach (var descriptor in collection!)
        {
            var des = descriptor as ModuleWebDescriptor;
            des?.Configurator?.ConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 处理模块Web的后置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleWebPostConfigureService(this IServiceCollection services, ModuleConfigureContext context)
    {
        services.AddModulePostService(context);
        var collection = services.GetModuleWebCollection();
        foreach (var descriptor in collection!)
        {
            var des = descriptor as ModuleWebDescriptor;
            des?.Configurator?.PostConfigureService(context);
        }

        return services;
    }
}