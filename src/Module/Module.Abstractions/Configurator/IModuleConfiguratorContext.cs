using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OpenModular.Module.Abstractions.Configurator;

/// <summary>
/// 模块配置器上下文
/// </summary>
public interface IModuleConfiguratorContext
{
    /// <summary>
    /// 服务集合
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// 环境变量
    /// </summary>
    IHostEnvironment Environment { get; }

    /// <summary>
    /// 配置对象
    /// </summary>
    IConfiguration Configuration { get; }

    /// <summary>
    /// 模块集合
    /// </summary>
    IModuleCollection Modules { get; }
}