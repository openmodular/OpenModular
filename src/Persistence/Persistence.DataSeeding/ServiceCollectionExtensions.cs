using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;
using OpenModular.Persistence.DataSeeding.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

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
        services.Configure<DataSeedingOptions>(opts =>
        {
            opts.DbFileName = DataSeedingConstants.DbFileName;
            opts.DbPassword = DataSeedingConstants.DbPassword;
        });

        services.AddScoped<IDataSeedingHandler, DefaultDataSeedingHandler<TDbContext>>();

        services.TryAddScoped<IDataSeedingExecutor, DefaultDataSeedingExecutor>();

        return services;
    }
}