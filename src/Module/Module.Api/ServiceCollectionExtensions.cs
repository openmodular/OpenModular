using Microsoft.Extensions.DependencyInjection;
using OpenModular.Module.Core;

namespace OpenModular.Module.Api;

public static class ServiceCollectionExtensions
{
    private static List<IModuleApiConfigurator>? _apiConfigurators = new();

    /// <summary>
    /// 添加模块化API服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModuleApiService(this IServiceCollection services)
    {
        services.AddModuleCoreService();

        services.AddSingleton<IModuleApiCollection>(new ModuleApiCollection());

        return services;
    }

    /// <summary>
    /// 获取模块API集合
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IModuleApiCollection GetModuleApiCollection(this IServiceCollection services)
    {
        return (IModuleApiCollection)services.First(m => m.ServiceType == typeof(IModuleApiCollection)).ImplementationInstance!;
    }

    /// <summary>
    /// 添加模块API
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleApi"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleApi(this IServiceCollection services, IModuleApi moduleApi)
    {
        services.AddModule(moduleApi.Module);

        var moduleApiCollection = services.GetModuleApiCollection();
        moduleApiCollection.Add(moduleApi);

        LoadModuleApiConfigurator(moduleApi);

        return services;
    }

    /// <summary>
    /// 处理模块API的前置服务注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddModuleApiPreConfigureService(this IServiceCollection services, ModuleConfigureContext context)
    {
        services.AddModulePreService(context);

        foreach (var moduleApi in _apiConfigurators!)
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
    /// <returns></returns>
    public static IServiceCollection AddModuleApiConfigureService(this IServiceCollection services, ModuleConfigureContext context)
    {
        services.AddModuleService(context);

        foreach (var moduleApi in _apiConfigurators!)
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
    /// <returns></returns>
    public static IServiceCollection AddModuleApiPostConfigureService(this IServiceCollection services, ModuleConfigureContext context)
    {
        services.AddModulePostService(context);

        foreach (var moduleApi in _apiConfigurators!)
        {
            moduleApi.PostConfigureService(context);
        }

        _apiConfigurators = null;

        return services;
    }

    /// <summary>
    /// 加载模块API配置器
    /// </summary>
    /// <param name="moduleApi"></param>
    private static void LoadModuleApiConfigurator(IModuleApi moduleApi)
    {
        var coreAssembly = moduleApi.GetType().Assembly;

        var configuratorType = coreAssembly.GetTypes().FirstOrDefault(m =>
            typeof(IModuleApiConfigurator).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract);

        if (configuratorType != null)
        {
            var configurator = (IModuleApiConfigurator)Activator.CreateInstance(configuratorType)!;

            _apiConfigurators!.Add(configurator);
        }
    }
}