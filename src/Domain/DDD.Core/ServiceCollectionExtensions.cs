using OpenModular.DDD.Core;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加领域驱动服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDDD(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile));

        return services;
    }
}