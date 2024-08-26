using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

/// <summary>
/// 配置仓储接口
/// </summary>
public interface IConfigRepository : IRepository<Config, ConfigId>;