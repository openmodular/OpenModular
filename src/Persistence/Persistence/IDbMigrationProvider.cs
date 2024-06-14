namespace OpenModular.Persistence;

/// <summary>
/// 数据迁移提供程序
/// </summary>
public interface IDbMigrationProvider
{
    /// <summary>
    /// 数据库架构迁移
    /// </summary>
    /// <returns></returns>
    Task SchemaMigrateAsync();

    /// <summary>
    /// 数据迁移
    /// </summary>
    /// <returns></returns>
    Task DataMigrateAsync();
}