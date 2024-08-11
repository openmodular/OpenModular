using Microsoft.Extensions.DependencyInjection;
using OpenModular.Config.Abstractions;

namespace OpenModular.Config.Core;   

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenModularConfig(this IServiceCollection services, Action<ConfigOptions> configure)
    {
        services.Configure(configure);
        services.AddSingleton<IConfigProvider, ConfigProvider>();
        return services;
    }
}