using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenModular.Cache.Abstractions;
using OpenModular.Common.Utils;
using OpenModular.Module.Core;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace OpenModular.Cache.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加缓存服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheOptions>(configuration.GetSection(CacheOptions.Position));

        using var sp = services.BuildServiceProvider();
        var options = sp.GetRequiredService<IOptions<CacheOptions>>().Value;

        if (options.Custom)
            return services;

        var modules = services.GetModuleCollection();

        foreach (var module in modules)
        {
            // 查找缓存提供器的实现
            var providerType = module.Module.GetType().Assembly.GetTypes()
                .FirstOrDefault(t => t.IsAssignableTo(typeof(ICacheProvider)));

            if (providerType == null)
                continue;

            var builder = services.AddFusionCache(module.Module.Code).WithOptions(op =>
            {
                op.CacheKeyPrefix = module.Module.Code;
            });

            builder.WithSerializer(new FusionCacheSystemTextJsonSerializer());

            if (options.Mode == CacheMode.Redis)
            {
                if (options.Redis == null || options.Redis.ConnectionString.IsNullOrWhiteSpace())
                    throw new ArgumentNullException(nameof(options.Redis));

                if (options.Redis.InstanceName.IsNullOrWhiteSpace())
                    options.Redis.InstanceName = OpenModularConstants.Name;

                builder.WithDistributedCache(new RedisCache(new RedisCacheOptions
                {
                    Configuration = options.Redis.ConnectionString,
                    InstanceName = options.Redis.InstanceName
                }));
            }

            builder.AsKeyedServiceByCacheName();

            services.AddSingleton(providerType);
            services.AddSingleton(typeof(ICacheProvider), providerType);
        }

        return services;
    }
}