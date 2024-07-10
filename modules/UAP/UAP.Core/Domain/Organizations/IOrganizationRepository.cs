using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Domain.Repositories;
using OpenModular.Module.UAP.Core.Domain.Organizations.Models;

namespace OpenModular.Module.UAP.Core.Domain.Organizations;

/// <summary>
/// 组织单位仓储
/// </summary>
internal interface IOrganizationRepository : IRepository<Organization, OrganizationId>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="model"></param>
    /// <param name="pagination"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedResult<Organization>> PageQueryAsync(OrganizationQueryModel model, Pagination pagination, CancellationToken cancellationToken);
}