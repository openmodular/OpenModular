using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenModular.Cache.Abstractions;
using OpenModular.Common.Utils;
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
            // 查找模块缓存器的实现
            var cacheType = module.Module.GetType().Assembly.GetTypes()
                .FirstOrDefault(t => t.IsAssignableTo(typeof(ICache)));

            if (cacheType == null)
                continue;

            var builder = services.AddFusionCache(module.Module.Code).WithOptions(op =>
            {
                op.CacheKeyPrefix = module.Module.Code;
            });

            builder.WithSerializer(new FusionCacheSystemTextJsonSerializer());

            if (options.Mode == CacheMode.Redis)
            {
                if (options.Redis == null || options.Redis.ConnectionString.IsNull())
                    throw new ArgumentNullException(nameof(options.Redis));

                if (options.Redis.InstanceName.IsNull())
                    options.Redis.InstanceName = OpenModularConstants.Name;

                builder.WithDistributedCache(new RedisCache(new RedisCacheOptions
                {
                    Configuration = options.Redis.ConnectionString,
                    InstanceName = options.Redis.InstanceName
                }));
            }

            builder.AsKeyedServiceByCacheName();

            services.AddSingleton(cacheType);
        }

        return services;
    }
}