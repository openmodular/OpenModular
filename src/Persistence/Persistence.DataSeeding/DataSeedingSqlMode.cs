namespace OpenModular.Persistence.DataSeeding;

/// <summary>
/// 数据迁移SQL模式
/// </summary>
public enum DataSeedingSqlMode
{
    Common, 
    Sqlite,
    SqlServer,
    PostgreSql,
    MySql,
}