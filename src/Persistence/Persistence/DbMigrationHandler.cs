namespace OpenModular.Persistence;

public class DbMigrationHandler(IEnumerable<IDbMigrationProvider> providers)
{
    /// <summary>
    /// 执行数据库迁移
    /// </summary>
    /// <returns></returns>
    public async Task MigrateAsync()
    {
        foreach (var provider in providers)
        {
            await provider.SchemaMigrateAsync();
        }

        foreach (var provider in providers)
        {
            await provider.DataMigrateAsync();
        }
    }
}