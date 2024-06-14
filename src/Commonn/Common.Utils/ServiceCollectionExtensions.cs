using Microsoft.Extensions.DependencyInjection;

namespace OpenModular.Common.Utils;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加OpenModular通用工具服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCommonUtils(this IServiceCollection services)
    {
        return services;
    }
}