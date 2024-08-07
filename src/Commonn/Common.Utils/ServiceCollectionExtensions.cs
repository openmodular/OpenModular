﻿using Mapster;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Common.Utils.DependencyInjection;

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
        services.AddMapster();

        services.AddFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    /// <summary>
    /// 从指定程序集中添加服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        if (assembly == null)
            return services;

        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (type.IsInterface || type.IsAbstract)
                continue;

            ServiceDescriptor descriptor;

            var interfaceType = type.GetInterfaces().FirstOrDefault(m => m != typeof(ITransientDependency) && m != typeof(ISingletonDependency) && m != typeof(IScopedDependency));
            if (interfaceType == null)
            {
                interfaceType = type;
            }

            if (typeof(ITransientDependency).IsAssignableFrom(type))
            {
                descriptor = new ServiceDescriptor(interfaceType, type, ServiceLifetime.Transient);
            }

            else if (typeof(ISingletonDependency).IsAssignableFrom(type))
            {

                descriptor = new ServiceDescriptor(interfaceType, type, ServiceLifetime.Singleton);

            }
            else if (typeof(IScopedDependency).IsAssignableFrom(type))
            {
                descriptor = new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped);
            }
            else
            {
                continue;
            }

            services.Add(descriptor);
        }

        return services;
    }
}