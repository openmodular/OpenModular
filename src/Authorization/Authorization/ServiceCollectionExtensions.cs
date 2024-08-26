﻿using OpenModular.Authorization;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加OpenModular授权服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenModularAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(OpenModularAuthorizationRequirement.Name, policy => policy.Requirements.Add(new OpenModularAuthorizationRequirement()));
        });

        return services;
    }
}