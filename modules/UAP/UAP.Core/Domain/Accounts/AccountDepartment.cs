using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Domain.Accounts;

/// <summary>
/// 用户所属部门
/// </summary>
public class AccountDepartment : Entity
{
    /// <summary>
    /// 用户编号
    /// </summary>
    public AccountId AccountId { get; }

    /// <summary>
    /// 部门编号
    /// </summary>
    public DepartmentId DepartmentId { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="departmentId"></param>
    public AccountDepartment(AccountId accountId, DepartmentId departmentId)
    {
        AccountId = accountId;
        DepartmentId = departmentId;
    }
}