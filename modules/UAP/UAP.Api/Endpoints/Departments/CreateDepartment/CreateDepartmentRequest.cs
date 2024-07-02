using System.ComponentModel.DataAnnotations;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Api.Endpoints.Departments.CreateDepartment;

/// <summary>
/// 创建部门请求
/// </summary>
public class CreateDepartmentRequest
{
    /// <summary>
    /// 组织编号
    /// </summary>
    public required OrganizationId OrganizationId { get; init; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// 父级部门
    /// </summary>
    public DepartmentId ParentId { get; init; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; init; }
}