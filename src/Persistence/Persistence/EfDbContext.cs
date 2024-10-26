using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Events;
using System.Linq.Expressions;
using MediatR;

namespace OpenModular.Persistence;

/// <summary>
/// EF数据库上下文
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public abstract class EfDbContext<TDbContext> : DbContext where TDbContext : DbContext
{
    public string ModuleCode { get; }

    private readonly IMediator _mediator;

    protected EfDbContext(DbContextOptions<TDbContext> dbContextOptions, string moduleCode, IMediator mediator) : base(dbContextOptions)
    {
        ModuleCode = moduleCode;
        _mediator = mediator;
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

    public override int SaveChanges()
    {
        var domainEvents = GetDomainEvents();
        var result = base.SaveChanges();
        PublishDomainEventsAsync(domainEvents).GetAwaiter().GetResult();
        return result;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var domainEvents = GetDomainEvents();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        PublishDomainEventsAsync(domainEvents).GetAwaiter().GetResult();
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var domainEvents = GetDomainEvents();
        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEventsAsync(domainEvents);
        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = GetDomainEvents();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await PublishDomainEventsAsync(domainEvents);
        return result;
    }

    private async Task PublishDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            entry.Entity.ClearDomainEvents();
        }
    }

    public List<IDomainEvent> GetDomainEvents()
    {
        return ChangeTracker
            .Entries<Entity>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();
    }
}