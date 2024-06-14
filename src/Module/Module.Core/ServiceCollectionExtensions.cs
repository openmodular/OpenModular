using Microsoft.Extensions.DependencyInjection;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加模块化服务
    /// </summary>
    /// <param name="services"></param>
    public static IModuleCollection AddModuleCoreService(this IServiceCollection services)
    {
        var collection = new ModuleCollection();

        services.AddSingleton<IModuleCollection>(collection);

        return collection;
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