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
}