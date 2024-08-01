﻿using System.Text.Json.Serialization;
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
    public string Code { get; private set; }

    /// <summary>
    /// 说明
    /// </summary>
    [JsonInclude]
    public string Description { get; private set; }

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
    [JsonInclude]
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Organization()
    {

    }

    [JsonConstructor]
    private Organization(OrganizationId id, string name, string code, string description, UserId createdBy) : base(id)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(code, nameof(code));

        Name = name;
        Code = code;
        Description = description;

        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public static Organization Create(string name, string code, string description, UserId createdBy)
    {
        return new Organization(new OrganizationId(), name, code, description, createdBy);
    }

    public void Rename(string name)
    {
        Name = name;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}