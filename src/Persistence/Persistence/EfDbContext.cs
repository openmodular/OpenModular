using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Domain.Entities;
using System.Linq.Expressions;

namespace OpenModular.Persistence;

/// <summary>
/// EF数据库上下文
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public abstract class EfDbContext<TDbContext> : DbContext where TDbContext : DbContext
{
    public string ModuleCode { get; }

    protected EfDbContext(string moduleCode, DbContextOptions<TDbContext> dbContextOptions) : base(dbContextOptions)
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
            case DbConstants.SqlServerProviderName:
                return DbProvider.SqlServer;
            case DbConstants.PostgreSQLProviderName:
                return DbProvider.PostgreSql;
            case DbConstants.MySqlProviderName:
                return DbProvider.MySql;
            case DbConstants.SqliteProviderName:
                return DbProvider.Sqlite;
            default:
                throw new NotSupportedException($"The database({Database.ProviderName}) not supported");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = GetType().Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //添加软删除过滤器
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var filter = Expression.Lambda(Expression.Equal(Expression.Property(parameter, nameof(ISoftDelete.IsDeleted)), Expression.Constant(false)), parameter);
                entityType.SetQueryFilter(filter);
            }
        }
    }
}