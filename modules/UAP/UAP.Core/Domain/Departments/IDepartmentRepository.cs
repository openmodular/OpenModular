using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Departments;

internal interface IDepartmentRepository : IRepository<Department, DepartmentId>
{
    /// <summary>
    /// 根据指定编码前缀查询部门列表
    /// </summary>
    /// <param name="codePrefix"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Department>> QueryByCodePrefixAsync(string codePrefix, CancellationToken cancellationToken = default);
}