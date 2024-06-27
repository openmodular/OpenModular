namespace OpenModular.Persistence.DataSeeding;

/// <summary>
/// 数据种子过滤器
/// </summary>
public interface IDataSeedingFilter
{
    /// <summary>
    /// 如果返回false，则后续不再对该数据进行处理
    /// </summary>
    /// <param name="record"></param>
    /// <returns></returns>
    Task<bool> Before(DataSeedingRecord record);

    /// <summary>
    /// 数据种子处理后调用
    /// </summary>
    /// <param name="record"></param>
    /// <returns></returns>
    Task After(DataSeedingRecord record);
}