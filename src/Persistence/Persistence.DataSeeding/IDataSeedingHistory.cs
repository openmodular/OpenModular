namespace OpenModular.Persistence.DataSeeding;

public interface IDataSeedingHistory
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