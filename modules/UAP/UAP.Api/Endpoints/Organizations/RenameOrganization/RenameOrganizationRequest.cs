using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Api.Endpoints.Organizations.RenameOrganization;

/// <summary>
/// 重命名组织请求
/// </summary>
public record RenameOrganizationRequest
{
    /// <summary>
    /// 组织标识
    /// </summary>
    public required OrganizationId OrganizationId { get; set; }

    /// <summary>
    /// 组织名称
    /// </summary>
    public required string Name { get; set; }
}