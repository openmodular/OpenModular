using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

/// <summary>
/// 用于模块中添加特有服务的上下文
/// </summary>
public class ModuleConfigureContext(IServiceCollection services, IHostEnvironment environment, IConfiguration configuration) : IModuleConfiguratorContext
{
    /// <summary>
    /// 服务集合
    /// </summary>
    public IServiceCollection Services { get; } = services;

    /// <summary>
    /// 环境变量
    /// </summary>
    public IHostEnvironment Environment { get; } = environment;

    /// <summary>
    /// 配置对象
    /// </summary>
    public IConfiguration Configuration { get; } = configuration;
}