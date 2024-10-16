using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Persistence;

public abstract class OpenModularDbContext<TDbContext> : DbContext where TDbContext : DbContext
{
    public string ModuleCode { get; }

    protected OpenModularDbContext(DbContextOptions<TDbContext> dbContextOptions, string moduleCode) : base(dbContextOptions)
    {
        ModuleCode = moduleCode;
    }

    /// <summary>
    /// 获取数据库类型
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public DbProvider GetDatabaseProvider()
    {
        switch (Database.ProviderName)
        {
            case OpenModularDbConstants.SqlServerProviderName:
                return DbProvider.SqlServer;
            case OpenModularDbConstants.PostgreSQLProviderName:
                return DbProvider.PostgreSql;
            case OpenModularDbConstants.MySqlProviderName:
                return DbProvider.MySql;
            case OpenModularDbConstants.SqliteProviderName:
                return DbProvider.Sqlite;
            default:
                throw new NotSupportedException($"The database({Database.ProviderName}) not supported");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = this.GetType().Assembly;

        var typeIds = assembly.GetTypes()
            .Where(type => typeof(TypedIdValueBase).IsAssignableFrom(type));
        foreach (var typeId in typeIds)
        {
            modelBuilder.Ignore(typeId);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}