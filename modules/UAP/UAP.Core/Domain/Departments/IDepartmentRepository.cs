using OpenModular.DDD.Core.Domain.Repositories;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Domain.Departments;

internal interface IDepartmentRepository : IRepository<Department, DepartmentId>
{
    /// <summary>
    /// 获取指定部门的最大子级编码
    /// </summary>
    /// <param name="organizationId"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetChildMaxCodeAsync(OrganizationId organizationId, DepartmentId parentId, CancellationToken cancellationToken);
}