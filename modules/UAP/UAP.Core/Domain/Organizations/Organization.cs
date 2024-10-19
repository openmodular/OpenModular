using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

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
    public string Code { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public AccountId CreatedBy { get; private set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }

    public Organization()
    {
        //for ef
    }

    private Organization(OrganizationId id, string name, string code, string description, AccountId createdBy) : base(id)
    {
        Check.NotNull(name, nameof(name));
        Check.NotNull(code, nameof(code));

        Name = name;
        Code = code;
        Description = description;

        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public static Organization Create(string name, string code, string description, AccountId createdBy)
    {
        return new Organization(new OrganizationId(), name, code, description, createdBy);
    }

    public static Organization Create(OrganizationId id, string name, string code, string description, AccountId createdBy)
    {
        return new Organization(id, name, code, description, createdBy);
    }

    public void Rename(string name)
    {
        Name = name;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}