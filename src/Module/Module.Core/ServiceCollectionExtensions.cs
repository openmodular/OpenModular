using Microsoft.Extensions.DependencyInjection;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

public static class ServiceCollectionExtensions
{
    private static ModuleDescriptorCollection _collection = new();

    /// <summary>
    /// 添加模块化服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModuleCoreService(this IServiceCollection services)
    {
        var collection = new ModuleDescriptorCollection();

        services.AddSingleton<IModuleDescriptorCollection>(collection);

        return services;
    }

    public static IServiceCollection AddModule(this IServiceCollection services, IModule module)
    {
        var descriptor = new ModuleDescriptor(module);

        _collection.Add(descriptor);

        return services;
    }

    /// <summary>
    /// 添加模块前置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="modules"></param>
    /// <returns></returns>
    public static IServiceCollection AddModulePreService(this IServiceCollection services, ModuleConfigureContext context, IModuleCollection modules)
    {
        foreach (var module in modules)
        {
            module.PreConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 添加模块服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="modules"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleService(this IServiceCollection services, ModuleConfigureContext context, IModuleCollection modules)
    {
        foreach (var module in modules)
        {
            module.ConfigureService(context);
        }

        return services;
    }

    /// <summary>
    /// 添加模块后置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="modules"></param>
    /// <returns></returns>
    public static IServiceCollection AddModulePostService(this IServiceCollection services, ModuleConfigureContext context, IModuleCollection modules)
    {
        foreach (var module in modules)
        {
            module.PostConfigureService(context);
        }

        return services;
    }
}