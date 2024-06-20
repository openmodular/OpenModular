namespace OpenModular.Persistence.DataSeeding.Builder;

/// <summary>
/// 种子数据定义
/// </summary>
public interface IDataSeedingDefinition
{
    IList<DataSeedingRecord> Get();
}