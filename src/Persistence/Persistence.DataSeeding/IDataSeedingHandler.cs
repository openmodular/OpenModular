namespace OpenModular.Persistence.DataSeeding;

/// <summary>
/// 数据种子处理程序
/// </summary>
public interface IDataSeedingHandler
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <returns></returns>
    Task DoAsync();
}