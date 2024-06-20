using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.Persistence.DataSeeding.Internal;

namespace OpenModular.Persistence.DataSeeding;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加数据种子
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataSeeding<TDbContext>(this IServiceCollection services) where TDbContext : OpenModularDbContext<TDbContext>
    {
        services.TryAddScoped<IDataSeedingExecutor, DefaultDataSeedingExecutor>();

        services.TryAddScoped<IDataSeedingHandler, DefaultDataSeedingHandler<TDbContext>>();

        return services;
    }
}