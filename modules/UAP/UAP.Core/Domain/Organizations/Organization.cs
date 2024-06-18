using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Domain.Organizations;

/// <summary>
/// 组织单位
/// </summary>
public class Organization : AggregateRoot<OrganizationId>
{
    /// <summary>
    /// 组织名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 组织编码
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// 说明
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public UserId CreatedBy { get; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; private set; }

    private Organization(string name, string code, string? description, UserId createdBy)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(code, nameof(code));

        Name = name;
        Code = code;
        Description = description;

        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public void Rename(string name)
    {
        Name = name;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}