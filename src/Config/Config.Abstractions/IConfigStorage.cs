namespace OpenModular.Config.Abstractions;

/// <summary>
/// 配置存储器，用于配置信息的持久化保存
/// </summary>
public interface IConfigStorage
{
    /// <summary>
    /// 保存配置
    /// </summary>
    /// <param name="config">配置信息</param>
    void Save(IConfig config);

    /// <summary>
    /// 读取配置
    /// </summary>
    /// <typeparam name="TConfig">配置类型</typeparam>
    /// <returns>配置信息</returns>
    TConfig Load<TConfig>() where TConfig : IConfig;
}