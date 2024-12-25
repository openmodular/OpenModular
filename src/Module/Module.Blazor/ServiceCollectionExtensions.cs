using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.Module.Blazor;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add module blazor service.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModuleBlazorService(this IServiceCollection services)
    {
        var modules = new ModuleBlazorCollection();

        services.TryAddSingleton<IModuleBlazorCollection>(modules);

        return services;
    }

    /// <summary>
    /// Get IModuleBlazorCollection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IModuleBlazorCollection GetModuleCollection(this IServiceCollection services)
    {
        return (IModuleBlazorCollection)services.First(m => m.ServiceType == typeof(IModuleBlazorCollection)).ImplementationInstance!;
    }

    /// <summary>
    /// Register a module blazor.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public static IModuleBlazorDescriptor RegisterModule(this IServiceCollection services, IModuleBlazor module)
    {
        var descriptor = new ModuleBlazorDescriptor(module);

        var collection = services.GetModuleCollection();
        collection.Add(descriptor);

        return descriptor;
    }
}