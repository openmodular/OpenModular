using Microsoft.Extensions.DependencyInjection;

namespace OpenModular.Common.Utils;

/// <summary>
/// 全局服务提供者
/// </summary>
public class GlobalServiceProvider
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public static T? GetService<T>()
    {
        return ServiceProvider.GetService<T>();
    }

    public static object? GetService(Type serviceType)
    {
        return ServiceProvider.GetService(serviceType);
    }

    public static T? GetKeyedServices<T>(string serviceKey)
    {
        return ServiceProvider.GetKeyedService<T>(serviceKey);
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    public static object GetRequiredService(Type serviceType)
    {
        return ServiceProvider.GetRequiredService(serviceType);
    }

    public static T GetRequiredKeyedService<T>(string serviceKey) where T : notnull
    {
        return ServiceProvider.GetRequiredKeyedService<T>(serviceKey);
    }
}