using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.Module.Abstractions;
using OpenModular.Module.Abstractions.Localization;
using OpenModular.Module.Core;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

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
    public static IModuleDescriptor RegisterModuleCore(this IServiceCollection services, IModule module)
    {
        var descriptor = new ModuleDescriptor(module);

        var collection = services.GetModuleCollection();
        collection.Add(descriptor);

        return descriptor;
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
            var assembly = descriptor.Module.GetType().Assembly;

            //对象映射
            services.AddAutoMapper(assembly);

            //服务注入
            context.Services.AddServicesFromAssembly(assembly);

            //多语言
            var localizerType = assembly.GetTypes().FirstOrDefault(m => m.IsAssignableTo(typeof(IModuleLocalizer)));
            if (localizerType != null)
            {
                context.Services.TryAddTransient(localizerType);
                context.Services.TryAddKeyedTransient(typeof(IModuleLocalizer), descriptor.Module.Code, localizerType);
            }

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