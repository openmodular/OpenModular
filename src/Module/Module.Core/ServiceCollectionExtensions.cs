using Microsoft.Extensions.DependencyInjection;
using OpenModular.Common.Utils;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

public static class ServiceCollectionExtensions
{
    private static List<IModuleConfigurator> _configuratorCollection = new();

    /// <summary>
    /// 添加模块化服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModuleCoreService(this IServiceCollection services)
    {
        services.AddSingleton<IModuleCollection>(new ModuleCollection());

        return services;
    }

    /// <summary>
    /// 添加模块
    /// </summary>
    /// <param name="services"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public static IServiceCollection AddModule(this IServiceCollection services, IModule module)
    {
        services.AddSingleton(module.GetType(), module);

        var moduleCollection = services.GetModuleCollection();
        moduleCollection.Add(module);

        LoadModuleConfigurator(module);

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
        foreach (var module in _configuratorCollection!)
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
    /// <returns></returns>
    public static IServiceCollection AddModuleService(this IServiceCollection services, ModuleConfigureContext context)
    {
        foreach (var module in _configuratorCollection!)
        {
            context.Services.AddFromAssembly(module.GetType().Assembly);

            module.ConfigureService(context);
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
        foreach (var module in _configuratorCollection!)
        {
            module.PostConfigureService(context);
        }

        _configuratorCollection = null;

        return services;
    }

    /// <summary>
    /// 加载模块配置器
    /// </summary>
    /// <param name="module"></param>
    private static void LoadModuleConfigurator(IModule module)
    {
        var coreAssembly = module.GetType().Assembly;

        var moduleConfiguratorType = coreAssembly.GetTypes().FirstOrDefault(m =>
            typeof(IModuleConfigurator).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract);

        if (moduleConfiguratorType != null)
        {
            var configurator = (IModuleConfigurator)Activator.CreateInstance(moduleConfiguratorType)!;

            _configuratorCollection!.Add(configurator);
        }
    }
}