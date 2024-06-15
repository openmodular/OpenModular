namespace OpenModular.Module.Abstractions;

/// <summary>
/// 模块配置器
/// </summary>
public interface IModuleConfigurator
{
    /// <summary>
    /// 前置配置服务
    /// </summary>
    void PreConfigureService(IModuleConfiguratorContext context);

    /// <summary>
    /// 配置服务
    /// </summary>
    void ConfigureService(IModuleConfiguratorContext context);

    /// <summary>
    /// 后置配置服务
    /// </summary>
    void PostConfigureService(IModuleConfiguratorContext context);
}