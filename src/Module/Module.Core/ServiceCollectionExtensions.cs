using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.Common.Utils;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加模块化服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModuleCoreService(this IServiceCollection services)
    {
        var modules = new ModuleCollection();

        services.TryAddSingleton<IModuleCollection>(modules);

        return services;
    }

    /// <summary>
    /// 注册模块核心功能
    /// </summary>
    /// <param name="services"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterModuleCore(this IServiceCollection services, IModule module)
    {
        var descriptor = new ModuleDescriptor(module);

        var collection = services.GetModuleCollection();
        collection.Add(descriptor);

        return services;
    }

    /// <summary>
    /// 获取模块集合
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IModuleCollection GetModuleCollection(this IServiceCollection services)
    {
        return (IModuleCollection)services.First(m => m.ServiceType == typeof(IModuleCollection)).ImplementationInstance!;
    }

    /// <summary>
    /// 添加模块前置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModulePreService(this IServiceCollection services, ModuleConfigureContext context)
    {
        var collection = services.GetModuleCollection();
        foreach (var descriptor in collection!)
        {
            TypeAdapterConfig.GlobalSettings.Scan(descriptor.Module.GetType().Assembly);

            context.Services.AddFromAssembly(descriptor.Module.GetType().Assembly);

            var des = descriptor as ModuleDescriptor;
            des?.Configurator?.PreConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 添加模块服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleService(this IServiceCollection services, ModuleConfigureContext context)
    {
        var collection = services.GetModuleCollection();
        foreach (var descriptor in collection!)
        {
            var des = descriptor as ModuleDescriptor;
            des?.Configurator?.ConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 添加模块后置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModulePostService(this IServiceCollection services, ModuleConfigureContext context)
    {
        var collection = services.GetModuleCollection();

        foreach (var descriptor in collection!)
        {
            var des = descriptor as ModuleDescriptor;
            des?.Configurator?.PostConfigureService(context);
        }

        return services;
    }
}