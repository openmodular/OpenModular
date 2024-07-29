using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Persistence;

public abstract class OpenModularDbContext<TDbContext> : DbContext where TDbContext : DbContext
{
    public string ModuleCode { get; }

    protected OpenModularDbContext(DbContextOptions<TDbContext> dbContextOptions, string ModuleCode) : base(dbContextOptions)
    {
        this.ModuleCode = ModuleCode;
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
            case "Microsoft.EntityFrameworkCore.SqlServer":
                return DbProvider.SqlServer;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                return DbProvider.PostgreSql;
            case "Pomelo.EntityFrameworkCore.MySql":
                return DbProvider.MySql;
            case "Microsoft.EntityFrameworkCore.Sqlite":
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