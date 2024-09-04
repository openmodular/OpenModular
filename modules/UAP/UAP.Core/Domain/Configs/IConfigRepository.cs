using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Domain.Repositories;
using OpenModular.Module.UAP.Core.Domain.Configs.Models;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

/// <summary>
/// 配置仓储接口
/// </summary>
public interface IConfigRepository : IRepository<Config, ConfigId>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="model"></param>
    /// <param name="pagination"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedResult<Config>> PagedQueryAsync(ConfigPagedQueryModel model, Pagination pagination, CancellationToken cancellationToken);
}