namespace OpenModular.Persistence.DataSeeding;

/// <summary>
/// 数据种子历史记录存储器
/// </summary>
public interface IDataSeedingHistoryStorage
{
    /// <summary>
    /// 查询指定模块的最后一个版本
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <returns></returns>
    Task<int> GetLastVersionAsync(string moduleCode);

    /// <summary>
    /// 插入版本
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    Task InsertVersionAsync(string moduleCode, int version);
}