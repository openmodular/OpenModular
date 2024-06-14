using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Domain.Users;

/// <summary>
/// 用户所属部门
/// </summary>
public class UserDepartment : Entity
{
    /// <summary>
    /// 用户编号
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// 部门编号
    /// </summary>
    public DepartmentId DepartmentId { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="departmentId"></param>
    public UserDepartment(UserId userId, DepartmentId departmentId)
    {
        UserId = userId;
        DepartmentId = departmentId;
    }
}