using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.Configuration.Abstractions;
using OpenModular.Configuration.Core;
using OpenModular.Module.Core;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加OpenModular配置服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularConfig(this IServiceCollection services)
    {
        var modules = services.GetModuleCollection();

        foreach (var module in modules)
        {
            var configType = module.Module.GetType().Assembly.GetTypes().FirstOrDefault(t => t.IsAssignableTo(typeof(IConfig)));

            if (configType == null)
            {
                continue;
            }

            var descriptor = new ConfigDescriptor(module.Module.Code, configType);

            module.SetConfigDescriptor(descriptor);

            services.TryAddTransient(configType, sp =>
            {
                var provider = sp.GetRequiredService<IConfigProvider>();

                return provider.GetAsync(configType).GetAwaiter().GetResult();
            });
        }

        return services;
    }
}