using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Departments;

/// <summary>
/// 部门编码生成器
/// </summary>
public interface IDepartmentCodeGenerator
{
    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="organization"></param>
    /// <param name="parent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GenerateAsync(Organization organization, Department parent, CancellationToken cancellationToken = default);
}
