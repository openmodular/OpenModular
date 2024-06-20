using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenModular.DDD.Core.Domain.Repositories;
using Microsoft.Data.Sqlite;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.Position));

        services.AddTransient<DbMigrationHandler>();

        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddOpenModularDbContext<TDbContext>(this IServiceCollection services) where TDbContext : OpenModularDbContext<TDbContext>
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<DbOptions>>().Value;

        var moduleCoreAssembly = typeof(TDbContext).Assembly;
        var moduleCoreAssemblyName = moduleCoreAssembly.GetName().Name!;
        string migrationsAssemblyPrefix = moduleCoreAssemblyName.Substring(0, moduleCoreAssemblyName.LastIndexOf('.'));

        switch (options.Provider)
        {
            case DbProvider.Sqlite:
                services.AddSQLite<TDbContext>(options.ConnectionString, migrationsAssemblyPrefix);
                break;
            case DbProvider.MySql:
                services.AddMySql<TDbContext>(options.ConnectionString, migrationsAssemblyPrefix);
                break;
            case DbProvider.PostgreSql:
                services.AddPostgreSql<TDbContext>(options.ConnectionString, migrationsAssemblyPrefix);
                break;
        }

        services.AddDbMigrationProvider(moduleCoreAssembly);

        services.AddRepositories(moduleCoreAssembly);

        return services;
    }

    private static void AddSQLite<TDbContext>(this IServiceCollection services, string connectionString, string migrationsAssemblyPrefix) where TDbContext : OpenModularDbContext<TDbContext>
    {
        services.AddDbContextPool<TDbContext>(builder =>
        {
            //将数据库相对路径转换为绝对路径
            var connectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);
            if (connectionStringBuilder.DataSource.IsNullOrWhiteSpace())
            {
                connectionStringBuilder.DataSource = "./Data/Database/OpenModular.db";
            }

            if (!Path.IsPathRooted(connectionStringBuilder.DataSource))
            {
                connectionStringBuilder.DataSource = Path.Combine(AppContext.BaseDirectory, connectionStringBuilder.DataSource);
            }

            var dir = Path.GetDirectoryName(connectionStringBuilder.DataSource);
            if (dir.NotNullAndWhiteSpace() && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            connectionString = connectionStringBuilder.ToString();

            builder.UseSqlite(connectionString, x => x.MigrationsAssembly($"{migrationsAssemblyPrefix}.Migrations.Sqlite"));

#if DEBUG
            builder.LogTo(Console.WriteLine);
#endif
        });
    }

    private static void AddMySql<TDbContext>(this IServiceCollection services, string connectionString, string migrationsAssemblyPrefix) where TDbContext : OpenModularDbContext<TDbContext>
    {
        services.AddDbContextPool<TDbContext>(builder =>
        {
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.MigrationsAssembly($"{migrationsAssemblyPrefix}.Migrations.MySQL"));

#if DEBUG
            builder.LogTo(Console.WriteLine);
#endif
        });
    }

    private static void AddPostgreSql<TDbContext>(this IServiceCollection services, string connectionString, string migrationsAssemblyPrefix) where TDbContext : OpenModularDbContext<TDbContext>
    {
        //该配置会导致老数据中日期类型存储的-infinity解析失败导致异常
        //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        services.AddDbContextPool<TDbContext>(builder =>
        {
            builder.UseNpgsql(connectionString, x => x.MigrationsAssembly($"{migrationsAssemblyPrefix}.Migrations.Postgresql"));

#if DEBUG
            builder.LogTo(Console.WriteLine);
#endif
        });
    }

    /// <summary>
    /// 注入数据库迁移提供器
    /// </summary>
    private static void AddDbMigrationProvider(this IServiceCollection services, Assembly assembly)
    {
        var dbMigrationProviderType = assembly.GetTypes().FirstOrDefault(t => typeof(IDbMigrationProvider).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        if (dbMigrationProviderType != null)
        {
            services.TryAddTransient(typeof(IDbMigrationProvider), dbMigrationProviderType);
        }
    }

    /// <summary>
    /// 注入仓储实现
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    private static void AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes();

        var repositoryInterfaceTypes = assembly.GetTypes().Where(t => typeof(IRepository).IsAssignableFrom(t) && t.IsInterface).ToList();
        if (repositoryInterfaceTypes.Any())
        {
            foreach (var repositoryInterfaceType in repositoryInterfaceTypes)
            {
                var repositoryImplementation = types.FirstOrDefault(t => repositoryInterfaceType.IsAssignableFrom(t) && !t.IsAbstract);

                if (repositoryImplementation != null)
                {
                    services.TryAddTransient(repositoryInterfaceType, repositoryImplementation);
                }
            }
        }
    }
}