namespace OpenModular.Config.Abstractions;

public interface IConfigProvider
{
    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <returns></returns>
    Task<TConfig> GetAsync<TConfig>() where TConfig : IConfig, new();

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <returns></returns>
    Task<IConfig> GetAsync(Type configType);

    /// <summary>
    /// 设置配置
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="config"></param>
    /// <returns></returns>
    Task SetAsync<TConfig>(TConfig config) where TConfig : IConfig, new();
}