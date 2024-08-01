using System.Text.Json.Serialization;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.Module.UAP.Core.Domain.Departments.Events;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Domain.Departments;

/// <summary>
/// 部门
/// </summary>
public class Department : AggregateRoot<DepartmentId>
{
    /// <summary>
    /// 所属组织编号
    /// </summary>
    public OrganizationId OrganizationId { get; private set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 父级部门
    /// </summary>
    public DepartmentId ParentId { get; private set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; private set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public UserId CreatedBy { get; private set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Department()
    {
        //for mapper or serializable
    }

    [JsonConstructor]
    private Department(OrganizationId organizationId, string name, DepartmentId parentId, string code, UserId createdBy) : base(new DepartmentId())
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(code, nameof(code));

        OrganizationId = organizationId;
        Name = name;
        ParentId = parentId;
        Code = code;

        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new DepartmentCreatedDomainEvent(this));
    }

    /// <summary>
    /// 创建部门
    /// </summary>
    /// <param name="organizationId"></param>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <param name="code"></param>
    /// <param name="createdBy"></param>
    /// <returns></returns>
    public static Department Create(OrganizationId organizationId, string name, DepartmentId parentId, string code, UserId createdBy)
    {
        return new Department(organizationId, name, parentId, code, createdBy);
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="order"></param>
    public void SetOrder(int order)
    {
        Order = order < 0 ? 0 : order;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// 重命名
    /// </summary>
    /// <param name="name"></param>
    public void Rename(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;

        UpdatedAt = DateTime.UtcNow;
    }
}