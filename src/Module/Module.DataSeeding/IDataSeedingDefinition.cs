using Microsoft.EntityFrameworkCore;

namespace OpenModular.Module.DataSeeding;

public interface IDataSeedingDefinition;

/// <summary>
/// 种子数据定义
/// </summary>
public interface IDataSeedingDefinition<in TDbContext> : IDataSeedingDefinition where TDbContext : DbContext
{
    Task Define(TDbContext dbContext);
}