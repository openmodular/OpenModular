using OpenModular.Module.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 获取模块集合
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IModuleCollection GetModuleCollection(this IServiceCollection services)
    {
        return (IModuleCollection)services.First(m => m.ServiceType == typeof(IModuleCollection)).ImplementationInstance!;
    }
}