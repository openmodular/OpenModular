﻿using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Departments.CreateDepartment;

/// <summary>
/// 创建部门命令
/// </summary>
public class CreateDepartmentCommand : CommandBase<DepartmentId>
{
    /// <summary>
    /// 组织标识
    /// </summary>
    public OrganizationId OrganizationId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 父级部门标识
    /// </summary>
    public DepartmentId ParentId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public UserId CreatedBy { get; set; }
}