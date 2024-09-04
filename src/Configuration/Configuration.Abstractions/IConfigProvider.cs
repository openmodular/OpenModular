namespace OpenModular.Configuration.Abstractions;

public interface IConfigProvider
{
    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <returns></returns>
    ValueTask<TConfig> GetAsync<TConfig>() where TConfig : IConfig, new();

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="configType"></param>
    /// <returns></returns>
    ValueTask<object> GetAsync(Type configType);

    /// <summary>
    /// 设置配置
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="config"></param>
    /// <returns></returns>
    ValueTask SetAsync<TConfig>(TConfig config) where TConfig : IConfig, new();
}